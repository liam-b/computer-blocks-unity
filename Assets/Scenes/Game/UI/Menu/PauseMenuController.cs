using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {
  public bool open = false;

  public GameObject filter;
  public GameObject mainMenu;
  public GameObject savesMenu;
  public List<GameObject> menus;

  void Start() {
    menus = new List<GameObject>() {
      mainMenu,
      savesMenu
    };
  }

  void LateUpdate() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      if (!open) Open();
      else Close();
    }
  }

  public void SetActiveMenu(GameObject activeMenu) {
    foreach (GameObject menu in menus) {
      menu.SetActive(false);
    }
    activeMenu.SetActive(true);
  }

  public void Open() {
    open = true;
    filter.SetActive(true);
    SetActiveMenu(mainMenu);
  }

  public void Close() {
    open = false;
    filter.SetActive(false);
    foreach (GameObject menu in menus) {
      menu.SetActive(false);
    }
  }
}
