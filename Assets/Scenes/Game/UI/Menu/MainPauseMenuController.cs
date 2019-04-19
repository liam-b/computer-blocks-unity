using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPauseMenuController : MenuController {
  public Button resumeButton;
  public Button savesButton;
  public Button exitButton;

  public PauseMenuController pauseMenu;

  void Start() {
    resumeButton.onClick.AddListener(ResumeButtonClicked);
    savesButton.onClick.AddListener(SavesButtonClicked);
    exitButton.onClick.AddListener(ExitButtonClicked);
  }

  void ResumeButtonClicked() {
    pauseMenu.Close();
  }

  void SavesButtonClicked() {
    pauseMenu.SetActiveMenu(pauseMenu.savesMenu);
  }

  void ExitButtonClicked() {
    Application.Quit();
  }
}
