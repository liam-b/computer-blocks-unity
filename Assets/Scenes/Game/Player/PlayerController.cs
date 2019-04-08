using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public BlockType selectedBlockType = BlockType.Cable;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) selectedBlockType = BlockType.Cable;
    if (Input.GetKeyDown(KeyCode.Alpha2)) selectedBlockType = BlockType.Source;
  }
}
