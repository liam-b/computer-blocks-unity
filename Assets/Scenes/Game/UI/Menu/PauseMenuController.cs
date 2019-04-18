using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {
  public bool paused = false;

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      SetPauseState(!paused);
    }
  }

  public void SetPauseState(bool state) {
    paused = state;
    foreach (Transform child in transform) {
      child.gameObject.SetActive(state);
    }
  }
}
