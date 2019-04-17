using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBlockController : PropagatingBlockController {
  public Sprite sprite;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Source;
    spriteRenderer.sprite = sprite;
    SetCharge(true);
  }

  public override List<BlockController> Propagate() {
    List<UpdatePath> newPaths = new List<UpdatePath>();
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();
    foreach (BlockController block in surroundingBlocks) {
      newPaths.Add(new UpdatePath(this, block));
    }

    List<BlockController> nextUpdateBlocks = FindPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }
}