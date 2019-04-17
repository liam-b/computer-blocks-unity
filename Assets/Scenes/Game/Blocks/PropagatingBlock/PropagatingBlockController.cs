using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropagatingBlockController : BlockController {
  public override abstract List<BlockController> Propagate();

  protected List<BlockController> FindPathDifferences(List<UpdatePath> oldPaths, List<UpdatePath> newPaths) {
    List<BlockController> nextUpdateBlocks = new List<BlockController>();
    foreach (UpdatePath path in oldPaths) {
      if (!path.InList(newPaths) && path.destination != null) nextUpdateBlocks.Add(path.destination);
    }

    foreach (UpdatePath path in newPaths) {
      if (!path.InList(oldPaths)) nextUpdateBlocks.Add(path.destination);
    }

    return nextUpdateBlocks;
  }
}
