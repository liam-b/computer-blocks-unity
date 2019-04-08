using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public BlockType selectedBlockType = BlockType.Cable;
  public int selectedRotation = BlockRotation.Right;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) selectedBlockType = BlockType.Cable;
    if (Input.GetKeyDown(KeyCode.Alpha2)) selectedBlockType = BlockType.Source;
    if (Input.GetKeyDown(KeyCode.Alpha3)) selectedBlockType = BlockType.Inverter;

    if (Input.GetKeyDown(KeyCode.R)) selectedRotation = (selectedRotation + 1) % 4;
  }
}
