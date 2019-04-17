using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void init(BlockPosition position) {
    base.init(position);
    type = BlockType.Inverter;
    spriteRenderer.sprite = sprite;
    transform.Rotate(Vector3.forward * 90 * -position.r);
  }

  public override List<BlockController> update() {
    List<BlockController> surroundingBlocks = getSurroundingBlocks();
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
        if (position.isFacing(block.position)) {
          if (isDirectional(block.type)) {
            if (!block.position.isFacing(position)) newPaths.Add(new UpdatePath(this, block));
          }
          else newPaths.Add(new UpdatePath(this, block));
        }
      }
    }

    setCharge(!destinationOfAnyPath);
    List<BlockController> nextUpdateBlocks = findPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }

  public override void setCharge(bool newCharge) {
    if (charge != newCharge) {
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
