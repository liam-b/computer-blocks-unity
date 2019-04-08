using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockController : MonoBehaviour {
  public BlockPosition position;
  public BlockType type;
  public bool charge;

  protected GridController grid;
  protected SpriteRenderer spriteRenderer;
  public List<UpdatePath> paths;

  public abstract List<BlockController> update();

  void Awake() {
    grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridController>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    paths = new List<UpdatePath>();
  }

  public virtual void init(BlockPosition position) {
    this.position = position;
  }

  public List<BlockController> getSurroundingBlocks() {
    List<BlockController> blocks = new List<BlockController>();
    addBlockToList(blocks, new BlockPosition(position.x + 1, position.y, position.l));
    addBlockToList(blocks, new BlockPosition(position.x - 1, position.y, position.l));
    addBlockToList(blocks, new BlockPosition(position.x, position.y + 1, position.l));
    addBlockToList(blocks, new BlockPosition(position.x, position.y - 1, position.l));
    return blocks;
  }

  private List<BlockController> addBlockToList(List<BlockController> list, BlockPosition position) {
    if (grid.blocks.ContainsKey(position)) {
      list.Add(grid.blocks[position]);
    }
    return list;
  }

  protected virtual void setCharge(bool newCharge) {
    charge = newCharge;
  }
}