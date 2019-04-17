using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedBlock {
  public int type;
  public bool charge;
  public SerializedBlockPosition position;
  public List<SerializedUpdatePath> paths;

  public SerializedBlock(BlockController block) {
    position = new SerializedBlockPosition(block.position);
    type = (int)block.type;
    charge = block.charge;

    paths = new List<SerializedUpdatePath>();
    foreach (UpdatePath path in block.paths) {
      paths.Add(new SerializedUpdatePath(path));
    }
  }
}
