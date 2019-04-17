using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
  public Dictionary<BlockPosition, BlockController> blocks;
  public int gridSize;
  public int layers;
  public float gridSpacing;

  private int iterations = 0;
  public int iterationsPerTick;
  public int maxIterations;
  public PlayerController player;

  public GameObject CableGameObject;
  public GameObject SourceGameObject;
  public GameObject InverterGameObject;
  public GameObject DelayGameObject;
  public GameObject ViaGameObject;

  public GameObject GridLineObject;

  void Start() {
    blocks = new Dictionary<BlockPosition, BlockController>();

    for (int i = 0; i < gridSize; i++) {
      CreateGridLine(Direction.Vertical, i, gridSize);
      CreateGridLine(Direction.Horizontal, i, gridSize);
    }
  }

  void FixedUpdate() {
    iterations += 1;
    if (iterations % iterationsPerTick == 0) PropagateTickUpdates();
  }

  private void CreateGridLine(Direction direction, int index, int size) {
    float scale = Mathf.Round(size / 2 * gridSpacing) * gridSpacing;
    Vector2 offset;
    if (direction == Direction.Vertical) {
      offset = new Vector2(scale - index * gridSpacing - 0.5f, 0);
    } else {
      offset = new Vector2(0, scale - index * gridSpacing - 0.5f);
    }

    GameObject gridLine = Instantiate(GridLineObject, transform);
    gridLine.GetComponent<GridLineController>().SetLine(direction, offset, size / 2, 1 - gridSpacing);
  }

  public Vector2 BlockToWorldPosition(BlockPosition position) {
    return new Vector2(position.x * gridSpacing, position.y * gridSpacing);
  }

  public BlockPosition WorldToBlockPosition(Vector2 position) {
    return new BlockPosition(Mathf.RoundToInt(position.x / gridSpacing), Mathf.RoundToInt(position.y / gridSpacing), player.selectedLayer);
  }

  public void PlaceBlock(BlockType type, BlockPosition position) {
    if (!blocks.ContainsKey(position)) {
      GameObject block = Instantiate(BlockPrefabFromType(type), BlockToWorldPosition(position), Quaternion.identity, transform);
      BlockController controller = block.GetComponent<BlockController>();
      controller.Init(position);
      controller.UpdateLayer(player.selectedLayer);
      blocks.Add(position, controller);

      foreach (BlockController surrounding in controller.GetSurroundingBlocks()) {
        surrounding.Propagate();
        surrounding.Tick(true);
      }
      PropagateBlockUpdate(controller);
    }
  }

  public void RemoveBlock(BlockPosition position) {
    if (blocks.ContainsKey(position)) {
      BlockController controller = blocks[position];
      List<BlockController> surroundingBlocks = controller.GetSurroundingBlocks();
      blocks.Remove(position);
      Destroy(controller.gameObject);

      foreach (BlockController block in surroundingBlocks) {
        PropagateBlockUpdate(block);
      }
    }
  }

  private void PropagateBlockUpdate(BlockController source) {
    Queue<BlockController> updateQueue = new Queue<BlockController>();
    updateQueue.Enqueue(source);

    int iterations = 0;
    while (updateQueue.Count > 0) {
      Queue<BlockController> nextUpdateQueue = new Queue<BlockController>();
      do {
        foreach (BlockController block in updateQueue.Dequeue().Propagate()) {
          nextUpdateQueue.Enqueue(block);
        }
      } while (updateQueue.Count > 0);

      updateQueue = nextUpdateQueue;
      if (iterations++ >= maxIterations) {
        RemoveBlock(source.position);
        break;
      }
    }
  }

  private void PropagateTickUpdates() {
    List<BlockController> nextBlockupdates = new List<BlockController>();
    foreach (BlockController block in blocks.Values) {
      if (block.type == BlockType.Delay) nextBlockupdates.AddRange(block.Tick(false));
    }

    foreach (BlockController block in nextBlockupdates) {
      PropagateBlockUpdate(block);
    }
  }

  private GameObject BlockPrefabFromType(BlockType type) {
    if (type == BlockType.Source) return SourceGameObject;
    if (type == BlockType.Inverter) return InverterGameObject;
    if (type == BlockType.Delay) return DelayGameObject;
    if (type == BlockType.Via) return ViaGameObject;
    return CableGameObject;
  }

  public void Deserialize(SerializedGrid grid) {
    layers = grid.layers;
    foreach (BlockController block in blocks.Values) Destroy(block.gameObject);
    blocks.Clear();

    foreach (SerializedBlock block in grid.blocks) {
      BlockPosition position = new BlockPosition(block.position);
      GameObject blockObject = Instantiate(BlockPrefabFromType((BlockType)block.type), BlockToWorldPosition(position), Quaternion.identity, transform);
      BlockController controller = blockObject.GetComponent<BlockController>();

      controller.Init(position);
      controller.UpdateLayer(player.selectedLayer);
      controller.SetCharge(block.charge);
      blocks.Add(position, controller);
    }

    foreach (SerializedBlock block in grid.blocks) {
      BlockPosition position = new BlockPosition(block.position);
      BlockController controller = blocks[position];

      foreach (SerializedUpdatePath path in block.paths) {
        controller.paths.Add(new UpdatePath(blocks[new BlockPosition(path.source)], blocks[new BlockPosition(path.destination)]));
      }
    }
  }
}
