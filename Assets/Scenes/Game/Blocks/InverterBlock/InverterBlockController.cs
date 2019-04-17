using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Inverter;
    spriteRenderer.sprite = sprite;
    transform.Rotate(Vector3.forward * 90 * -position.r);
  }

  public override List<BlockController> Propagate() {
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();
    List<UpdatePath> newPaths = new List<UpdatePath>();

    bool destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          destinationOfAnyPath = true;
          break;
        }
      }
    }

    if (!destinationOfAnyPath) {
      foreach (BlockController block in surroundingBlocks) {
        if (position.IsFacing(block.position)) {
          if (TypeIsDirectional(block.type)) {
            if (!block.position.IsFacing(position)) newPaths.Add(new UpdatePath(this, block));
          }
          else newPaths.Add(new UpdatePath(this, block));
        }
      }
    }

    SetCharge(!destinationOfAnyPath);
    List<BlockController> nextUpdateBlocks = FindPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }

  public override void SetCharge(bool newCharge) {
    if (charge != newCharge) {
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
