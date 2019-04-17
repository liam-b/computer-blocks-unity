using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedBlockPosition {
  public int x, y, l, r;

  public SerializedBlockPosition(BlockPosition position) {
    x = position.x;
    y = position.y;
    l = position.l;
    r = position.r;
  }
}
