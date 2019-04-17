using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedUpdatePath {
  public SerializedBlockPosition source;
  public SerializedBlockPosition destination;

  public SerializedUpdatePath(UpdatePath block) {
    source = new SerializedBlockPosition(block.source.position);
    destination = new SerializedBlockPosition(block.destination.position);
  }
}
