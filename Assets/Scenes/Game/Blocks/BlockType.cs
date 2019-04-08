using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType {
  public static BlockType Cable = new BlockType(0);
  public static BlockType Source = new BlockType(1);
  public static BlockType Inverter = new BlockType(2);
  public static BlockType Delay = new BlockType(3);

  private int type;

  private BlockType(int type) {
    this.type = type;
  }

  public bool isDirectional() {
    return type == Inverter.type;
  }

  public override string ToString() {
    if (type == Source.type) return "Source";
    if (type == Inverter.type) return "Inverter";
    if (type == Delay.type) return "Delay";
    return "Cable";
  }
}
