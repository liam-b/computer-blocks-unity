﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  private bool destinationOfAnyPath = false;
  private bool shouldTick = false;

  public override void init(BlockPosition position) {
    base.init(position);
    type = BlockType.Delay;
    spriteRenderer.sprite = sprite;
    transform.Rotate(Vector3.forward * 90 * -position.r);
  }

  public override List<BlockController> update() {
    List<BlockController> surroundingBlocks = getSurroundingBlocks();

    destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          if (block.type.isDirectional()) {
            if (block.position.isFacing(position)) {
              destinationOfAnyPath = true;
              break;
            }
          } else {
            destinationOfAnyPath = true;
            break;
          }
        }
      }
    }

    setCharge(destinationOfAnyPath);
    return new List<BlockController>();
  }

  public override List<BlockController> tick() {
    List<UpdatePath> newPaths = new List<UpdatePath>();
    List<BlockController> nextUpdateBlocks = new List<BlockController>();

    if (shouldTick) {
      if (destinationOfAnyPath) {
        List<BlockController> surroundingBlocks = getSurroundingBlocks();
        foreach (BlockController block in surroundingBlocks) {
          if (position.isFacing(block.position)) {
            newPaths.Add(new UpdatePath(this, block));
          }
        }
      }

      nextUpdateBlocks = findPathDifferences(paths, newPaths);
      paths = newPaths;
    }

    shouldTick = false;
    return nextUpdateBlocks;
  }

  protected override void setCharge(bool newCharge) {
    if (charge != newCharge) {
      shouldTick = true;
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
