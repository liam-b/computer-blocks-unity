using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockController : MonoBehaviour {
  public BlockPosition position;
  public BlockType type;
  public bool charge;
  public bool shouldTick;
  public bool destinationOfAnyPath;

  protected GridController grid;
  protected SpriteRenderer spriteRenderer;
  public List<UpdatePath> paths;

  public abstract List<BlockController> Propagate();
  public virtual List<BlockController> Tick(bool forcePropagate) { return new List<BlockController>(); }

  void Awake() {
    grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    paths = new List<UpdatePath>();
  }

  public virtual void Init(BlockPosition position) {
    this.position = position;
  }

  public virtual List<BlockController> GetSurroundingBlocks() {
    List<BlockController> blocks = new List<BlockController>();
    AddBlockToList(blocks, new BlockPosition(position.x + 1, position.y, position.l));
    AddBlockToList(blocks, new BlockPosition(position.x - 1, position.y, position.l));
    AddBlockToList(blocks, new BlockPosition(position.x, position.y + 1, position.l));
    AddBlockToList(blocks, new BlockPosition(position.x, position.y - 1, position.l));
    return blocks;
  }

  protected List<BlockController> AddBlockToList(List<BlockController> list, BlockPosition position) {
    if (grid.blocks.ContainsKey(position)) {
      list.Add(grid.blocks[position]);
    }
    return list;
  }

  public void UpdateLayer(int layer) {
    // spriteRenderer.enabled = position.l == layer;

    if (position.l == layer) {
      spriteRenderer.color = Color.white;
      spriteRenderer.sortingOrder = 1;
    }
    else {
      spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
      spriteRenderer.sortingOrder = 0;
    }
  }

  protected bool TypeIsDirectional(BlockType type) {
    return type == BlockType.Inverter || type == BlockType.Delay;
  }

  public virtual void SetCharge(bool newCharge) {
    charge = newCharge;
  }
}