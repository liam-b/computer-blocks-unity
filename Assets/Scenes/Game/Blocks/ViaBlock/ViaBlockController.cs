using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViaBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void Init(BlockPosition position) {
    base.Init(position);
    type = BlockType.Via;
    spriteRenderer.sprite = sprite;
  }

  public override List<BlockController> Propagate() {
    List<BlockController> surroundingBlocks = GetSurroundingBlocks();
    List<UpdatePath> newPaths = new List<UpdatePath>();

    destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      if (block.position.l == position.l || (block.position.l != position.l && block.type == BlockType.Via)) {
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
    }

    SetCharge(destinationOfAnyPath);
    List<BlockController> nextUpdateBlocks = FindPathDifferences(paths, newPaths);
    paths = newPaths;
    return nextUpdateBlocks;
  }

  public override List<BlockController> GetSurroundingBlocks() {
    List<BlockController> blocks = base.GetSurroundingBlocks();
    AddBlockToList(blocks, new BlockPosition(position.x, position.y, position.l + 1));
    AddBlockToList(blocks, new BlockPosition(position.x, position.y, position.l - 1));
    return blocks;
  }

  public override void SetCharge(bool newCharge) {
    if (charge != newCharge) {
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
