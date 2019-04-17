using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Delay;
    spriteRenderer.sprite = sprite;
    transform.Rotate(Vector3.forward * 90 * -position.r);
  }

  public override List<BlockController> Propagate() {
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();

    destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          destinationOfAnyPath = true;
          break;
        }
      }
    }

    SetCharge(destinationOfAnyPath);
    return new List<BlockController>();
  }

  public override List<BlockController> Tick(bool forcePropagate) {
    List<UpdatePath> newPaths = new List<UpdatePath>();
    List<BlockController> nextUpdateBlocks = new List<BlockController>();

    if (shouldTick || forcePropagate) {
      if (destinationOfAnyPath) {
        List<BlockController> surroundingBlocks = GetSurroundingBlocks();
        foreach (BlockController block in surroundingBlocks) {
          if (position.IsFacing(block.position)) {
            newPaths.Add(new UpdatePath(this, block));
          }
        }
      }

      nextUpdateBlocks = FindPathDifferences(paths, newPaths);
      paths = newPaths;
    }

    shouldTick = false;
    return nextUpdateBlocks;
  }

  public override void SetCharge(bool newCharge) {
    if (charge != newCharge) {
      shouldTick = true;
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
