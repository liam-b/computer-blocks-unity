using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPosition {
  public int x { get; }
  public int y { get; }
  public int l { get; }

  private int hashCode;
  private static int hashRange = 10000;

  public BlockPosition(int x, int y, int l) {
    hashCode = generateHashCode();
    this.x = x;
    this.y = y;
    this.l = l;
  }

  public override int GetHashCode() {
    return hashCode;
  }

  public override bool Equals(object obj) {
    return Equals(obj as BlockPosition);
  }

  public bool Equals(BlockPosition position) {
    return position != null && position.x == x && position.y == y && position.l == l;
  }

  public override string ToString() {
    return x.ToString() + ',' + y.ToString() + ',' + l.ToString();
  }

  public int generateHashCode() {
    uint value = ((uint)(x + hashRange) * (uint)hashRange * 2 + (uint)(y + hashRange)) * 10 + (uint)l;
    return (int)value;
  }
}
