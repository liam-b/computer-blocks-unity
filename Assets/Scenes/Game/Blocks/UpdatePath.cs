using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePath {
  public BlockController source;
  public BlockController destination;

  public UpdatePath(BlockController source, BlockController destination) {
    this.source = source;
    this.destination = destination;
  }

  public bool equals(UpdatePath path) {
    return source == path.source && destination == path.destination;
  }

  public bool InList(List<UpdatePath> list) {
    bool foundPath = false;
    foreach (UpdatePath path in list) {
      if (equals(path)) foundPath = true;
    }

    return foundPath;
  }
}
