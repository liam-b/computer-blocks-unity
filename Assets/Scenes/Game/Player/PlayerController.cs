using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public BlockType selectedBlockType = BlockType.Cable;
  public int selectedRotation = BlockRotation.Right;
  public int selectedLayer = 0;

  public GridController grid;

  void Propagate() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) selectedBlockType = BlockType.Cable;
    if (Input.GetKeyDown(KeyCode.Alpha2)) selectedBlockType = BlockType.Source;
    if (Input.GetKeyDown(KeyCode.Alpha3)) selectedBlockType = BlockType.Inverter;
    if (Input.GetKeyDown(KeyCode.Alpha4)) selectedBlockType = BlockType.Delay;
    if (Input.GetKeyDown(KeyCode.Alpha5)) selectedBlockType = BlockType.Via;

    if (Input.GetKeyDown(KeyCode.R)) selectedRotation = (selectedRotation + 1) % 4;
    if (Input.GetKeyDown(KeyCode.LeftBracket)) selectedLayer = max(selectedLayer - 1, 0);
    if (Input.GetKeyDown(KeyCode.RightBracket)) selectedLayer = min(selectedLayer + 1, grid.layers - 1);

    if (Input.GetMouseButton(0)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      BlockPosition blockPosition = grid.WorldToBlockPosition(position);
      grid.PlaceBlock(selectedBlockType, new BlockPosition(blockPosition.x, blockPosition.y, selectedLayer, selectedRotation));
    }

    if (Input.GetMouseButton(1)) {
      Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      grid.RemoveBlock(grid.WorldToBlockPosition(position));
    }

    if (Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetKeyDown(KeyCode.RightBracket)) {
      foreach (BlockController block in grid.blocks.Values) {
        block.UpdateLayer(selectedLayer);
      }
    }

    if (Input.GetKeyDown(KeyCode.LeftShift)) {
      string json = JsonUtility.ToJson(new SerializedGrid(grid));

      StreamWriter writer = new StreamWriter(Path.Combine(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.json"));
      writer.Write(json);
      writer.Close();

      StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath + Path.DirectorySeparatorChar + "save.json"));
      json = reader.ReadToEnd();
      SerializedGrid sGrid = JsonUtility.FromJson<SerializedGrid>(json);
      reader.Close();

      grid.Deserialize(sGrid);
    }
  }

  private int max(int a, int b) {
    if (a > b) return a;
    return b;
  }

  private int min(int a, int b) {
    if (a < b) return a;
    return b;
  }
}
