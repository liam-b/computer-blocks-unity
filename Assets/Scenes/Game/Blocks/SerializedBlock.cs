using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedBlock {
  public SerializedBlockPosition position;
  public int type;
  public bool charge;
  public bool shouldTick;
  public bool destinationOfAnyPath;

  public List<SerializedUpdatePath> paths;

  public SerializedBlock(BlockController block) {
    position = new SerializedBlockPosition(block.position);
    type = (int)block.type;
    charge = block.charge;
    shouldTick = block.shouldTick;
    destinationOfAnyPath = block.destinationOfAnyPath;

    paths = new List<SerializedUpdatePath>();
    foreach (UpdatePath path in block.paths) {
      paths.Add(new SerializedUpdatePath(path));
    }
  }
}
