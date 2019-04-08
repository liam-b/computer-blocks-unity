using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropagatingBlockController : BlockController {
  public override List<BlockController> update() {
    List<BlockController> surroundingBlocks = getSurroundingBlocks();
    List<UpdatePath> newPaths = new List<UpdatePath>();

    bool destinationOfAnyPath = false;
    foreach (BlockController block in surroundingBlocks) {
      bool destinationOfPath = false;
      foreach (UpdatePath path in block.paths) {
        if (path.destination == this) {
          destinationOfPath = true;
          break;
        }
      }

      if (destinationOfPath) {
        destinationOfAnyPath = true;
        foreach (BlockController surrounding in surroundingBlocks) {
          if (surrounding.position != block.position) newPaths.Add(new UpdatePath(block, surrounding));
        }
      }
    }

    setCharge(destinationOfAnyPath);

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
