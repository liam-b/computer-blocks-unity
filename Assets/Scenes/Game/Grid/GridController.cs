using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
  private static int MAX_ITERATIONS = 10000;

  public Dictionary<BlockPosition, BlockController> blocks;
  public int gridSize;
  public int layers;
  public float gridSpacing;

  private int iterations = 0;
  public int iterationsPerTick;
  public PlayerController player;

  public GameObject CableGameObject;
  public GameObject SourceGameObject;
  public GameObject InverterGameObject;
  public GameObject DelayGameObject;
  public GameObject GridLineObject;

  void Start() {
    blocks = new Dictionary<BlockPosition, BlockController>();

    for (int i = 0; i < gridSize; i++) {
      createGridLine(Direction.Vertical, i, gridSize);
      createGridLine(Direction.Horizontal, i, gridSize);
    }
  }

  void Update() {
    if (Input.GetMouseButton(0)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      BlockPosition blockPosition = worldToBlockPosition(position);
      placeBlock(player.selectedBlockType, new BlockPosition(blockPosition.x, blockPosition.y, 0, player.selectedRotation));
    }

    if (Input.GetMouseButton(1)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      removeBlock(worldToBlockPosition(position));
    }
  }

  void FixedUpdate() {
    iterations += 1;
    if (iterations % iterationsPerTick == 0) tick();
  }

  private void createGridLine(Direction direction, int index, int size) {
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

  public Vector2 blockToWorldPosition(BlockPosition position) {
    return new Vector2(position.x * gridSpacing, position.y * gridSpacing);
  }

  public BlockPosition worldToBlockPosition(Vector2 position) {
    return new BlockPosition(Mathf.RoundToInt(position.x / gridSpacing), Mathf.RoundToInt(position.y / gridSpacing), 0);
  }

  public void placeBlock(BlockType type, BlockPosition position) {
    if (!blocks.ContainsKey(position)) {
      GameObject block = Instantiate(blockPrefabFromType(type), blockToWorldPosition(position), Quaternion.identity, transform);
      BlockController controller = block.GetComponent<BlockController>();
      controller.init(position);
      blocks.Add(position, controller);

      foreach (BlockController surrounding in controller.getSurroundingBlocks()) surrounding.update();
      propagateBlockUpdate(controller);
    }
  }

  public void removeBlock(BlockPosition position) {
    if (blocks.ContainsKey(position)) {
      BlockController controller = blocks[position];
      List<BlockController> surroundingBlocks = controller.getSurroundingBlocks();
      blocks.Remove(position);
      Destroy(controller.gameObject);

      foreach (BlockController block in surroundingBlocks) propagateBlockUpdate(block);
    }
  }

  private void propagateBlockUpdate(BlockController source) {
    Queue<BlockController> updateQueue = new Queue<BlockController>();
    updateQueue.Enqueue(source);

    int iterations = 0;
    while (updateQueue.Count > 0) {
      Queue<BlockController> nextUpdateQueue = new Queue<BlockController>();
      do {
        foreach (BlockController block in updateQueue.Dequeue().update()) {
          nextUpdateQueue.Enqueue(block);
        }
      } while (updateQueue.Count > 0);

      updateQueue = nextUpdateQueue;
      if (iterations++ >= MAX_ITERATIONS) {
        removeBlock(source.position);
        break;
      }
    }
  }

  private void tick() {
    List<BlockController> nextBlockupdates = new List<BlockController>();
    foreach (BlockController block in blocks.Values) {
      if (block.type == BlockType.Delay) nextBlockupdates.AddRange(block.tick());
    }

    foreach (BlockController block in nextBlockupdates) {
      propagateBlockUpdate(block);
    }
  }

  private GameObject blockPrefabFromType(BlockType type) {
    if (type == BlockType.Source) return SourceGameObject;
    if (type == BlockType.Inverter) return InverterGameObject;
    if (type == BlockType.Delay) return DelayGameObject;
    return CableGameObject;
  }
}
