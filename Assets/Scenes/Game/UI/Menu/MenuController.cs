using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
  private bool menuOpen = false;

  public bool open {
    get {
      return menuOpen;
    }

    set {
      menuOpen = value;
      foreach (Transform child in transform) {
        child.gameObject.SetActive(value);
      }
    }
  }
}