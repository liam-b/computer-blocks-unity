﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropagatingBlockController : BlockController {
  public override abstract List<BlockController> update();

  protected List<BlockController> findPathDifferences(List<UpdatePath> oldPaths, List<UpdatePath> newPaths) {
    List<BlockController> nextUpdateBlocks = new List<BlockController>();
    foreach (UpdatePath path in oldPaths) {
      if (!path.inList(newPaths)) nextUpdateBlocks.Add(path.destination); // might need to check if it exists
    }

    foreach (UpdatePath path in newPaths) {
      if (!path.inList(oldPaths)) nextUpdateBlocks.Add(path.destination);
    }

    return nextUpdateBlocks;
  }
}
