using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedGrid {
  public int layers;
  public List<SerializedBlock> blocks;

  public SerializedGrid(GridController grid) {
    layers = grid.layers;

    blocks = new List<SerializedBlock>();
    foreach (BlockController block in grid.blocks.Values) {
      blocks.Add(new SerializedBlock(block));
    }
  }
}