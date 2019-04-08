using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation {
  public static int Right = 0;
  public static int Up = 1;
  public static int Left = 2;
  public static int Down = 3;
}

public class BlockPosition {
  public int x { get; }
  public int y { get; }
  public int l { get; }
  public int r { get; }

  private int hashCode;
  private static int hashRange = 10000;

  public BlockPosition(int x, int y, int l, int r) {
    this.x = x;
    this.y = y;
    this.l = l;
    this.r = r;
    hashCode = generateHashCode();
  }

  public BlockPosition(int x, int y, int l) {
    this.x = x;
    this.y = y;
    this.l = l;
    this.r = 0;
    hashCode = generateHashCode();
  }

  public BlockPosition(int x, int y) {
    this.x = x;
    this.y = y;
    this.l = 0;
    this.r = 0;
    hashCode = generateHashCode();
  }

  public bool isFacing(BlockPosition position) {
    if (r == BlockRotation.Right) return position.x == x + 1 && position.y == y;
    if (r == BlockRotation.Up) return position.x == x && position.y + 1 == y;
    if (r == BlockRotation.Left) return position.x == x - 1 && position.y == y;
    if (r == BlockRotation.Down) return position.x == x && position.y - 1 == y;
    return false;
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
