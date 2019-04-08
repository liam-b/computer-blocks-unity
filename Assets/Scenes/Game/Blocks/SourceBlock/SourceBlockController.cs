using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceBlockController : BlockController {
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

    List<BlockController> nextUpdateBlocks = new List<BlockController>();
    foreach (UpdatePath path in paths) {
      if (!path.inList(newPaths)) {
        nextUpdateBlocks.Add(path.destination); // might need to check if it exists
      }
    }

    foreach (UpdatePath path in newPaths) {
      if (!path.inList(paths)) {
        nextUpdateBlocks.Add(path.destination);
      }
    }

    paths = newPaths;
    return nextUpdateBlocks;
  }
}