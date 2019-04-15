using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void init(BlockPosition position) {
    base.init(position);
    type = BlockType.Cable;
    spriteRenderer.sprite = sprite;
  }

  public override List<BlockController> update() {
    List<BlockController> surroundingBlocks = getSurroundingBlocks();
    List<UpdatePath> newPaths = new List<UpdatePath>();

    bool destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      bool destinationOfBlock = false;
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          destinationOfBlock = true;
          break;
        }
      }

      if (destinationOfBlock) {
        destinationOfAnyPath = true;
        foreach (BlockController surrounding in surroundingBlocks) {
          if (surrounding.position != block.position) {
            if (surrounding.type.isDirectional()) {
              if (!surrounding.position.isFacing(position)) newPaths.Add(new UpdatePath(block, surrounding));
            }
            else newPaths.Add(new UpdatePath(block, surrounding));
          }
        }
      }
    }

    setCharge(destinationOfAnyPath);
    List<BlockController> nextUpdateBlocks = findPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }

  protected override void setCharge(bool newCharge) {
    if (charge != newCharge) {
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
