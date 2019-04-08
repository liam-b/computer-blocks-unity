using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBlockController : PropagatingBlockController {
  public Sprite sprite;

  public override void init(BlockPosition position) {
    base.init(position);
    type = BlockType.Source;
    spriteRenderer.sprite = sprite;
    setCharge(true);
  }

  public override List<BlockController> update() {
    List<UpdatePath> newPaths = new List<UpdatePath>();
    List<BlockController> surroundingBlocks = getSurroundingBlocks();
    foreach (BlockController block in surroundingBlocks) {
      newPaths.Add(new UpdatePath(this, block));
    }

    List<BlockController> nextUpdateBlocks = findPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }
}