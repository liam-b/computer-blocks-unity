using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Cable;
    spriteRenderer.sprite = sprite;
  }

  public override List<BlockController> Propagate() {
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();
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
            if (TypeIsDirectional(surrounding.type)) {
              if (!surrounding.position.IsFacing(position)) newPaths.Add(new UpdatePath(block, surrounding));
            }
            else newPaths.Add(new UpdatePath(block, surrounding));
          }
        }
      }
    }

    SetCharge(destinationOfAnyPath);
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
