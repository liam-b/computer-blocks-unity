using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public BlockType selectedBlockType = BlockType.Cable;
  public int selectedRotation = BlockRotation.Right;
  public int selectedLayer = 0;

  public GridController grid;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) selectedBlockType = BlockType.Cable;
    if (Input.GetKeyDown(KeyCode.Alpha2)) selectedBlockType = BlockType.Source;
    if (Input.GetKeyDown(KeyCode.Alpha3)) selectedBlockType = BlockType.Inverter;
    if (Input.GetKeyDown(KeyCode.Alpha4)) selectedBlockType = BlockType.Delay;

    if (Input.GetKeyDown(KeyCode.R)) selectedRotation = (selectedRotation + 1) % 4;
    if (Input.GetKeyDown(KeyCode.LeftBracket)) selectedLayer = (selectedLayer - 1) % grid.layers;
    if (Input.GetKeyDown(KeyCode.RightBracket)) selectedLayer = (selectedLayer + 1) % grid.layers;

    if (Input.GetMouseButton(0)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      BlockPosition blockPosition = grid.worldToBlockPosition(position);
      grid.placeBlock(selectedBlockType, new BlockPosition(blockPosition.x, blockPosition.y, selectedLayer, selectedRotation));
    }

    if (Input.GetMouseButton(1)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      grid.removeBlock(grid.worldToBlockPosition(position));
    }

    if (Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetKeyDown(KeyCode.RightBracket)) {
      foreach (BlockController block in grid.blocks.Values) {
        block.changeLayer(selectedLayer);
      }
    }
  }
}
