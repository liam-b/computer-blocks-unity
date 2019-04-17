using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  private List<BlockController> destinationOfBlocks;
  private bool shouldTick = false;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Delay;
    spriteRenderer.sprite = sprite;
    transform.Rotate(Vector3.forward * 90 * -position.r);
  }

  public override List<BlockController> Propagate() {
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();

    destinationOfBlocks = new List<BlockController>();
    foreach (BlockController block in surroundingBlocks) {
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          destinationOfBlocks.Add(block);
          break;
        }
      }
    }

    SetCharge(destinationOfBlocks.Count > 0);
    return new List<BlockController>();
  }

  public override List<BlockController> Tick(bool forcePropagate) {
    List<UpdatePath> newPaths = new List<UpdatePath>();
    List<BlockController> nextUpdateBlocks = new List<BlockController>();

    if (shouldTick || forcePropagate) {
      if (destinationOfBlocks.Count > 0) {
        List<BlockController> surroundingBlocks = GetSurroundingBlocks();
        foreach (BlockController block in surroundingBlocks) {
          if (position.IsFacing(block.position) && !destinationOfBlocks.Contains(block)) {
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
