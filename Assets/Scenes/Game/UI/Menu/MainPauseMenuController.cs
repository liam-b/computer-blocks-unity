using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPauseMenuController : MonoBehaviour {
  public Button resumeButton;
  public Button exitButton;
  public PauseMenuController pauseMenu;

  void Start() {
    resumeButton.onClick.AddListener(ResumeButtonClicked);
    exitButton.onClick.AddListener(ExitButtonClicked);
  }

  void ResumeButtonClicked() {
    pauseMenu.SetPauseState(false);
  }

  void ExitButtonClicked() {
    Application.Quit();
  }
}
