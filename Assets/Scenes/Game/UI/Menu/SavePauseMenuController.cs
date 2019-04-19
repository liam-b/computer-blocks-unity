using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SavePauseMenuController : MenuController {
  public static string SavePath;
  public static string SaveExtension = ".json";

  public Button newSaveButton;
  public PauseMenuController pauseMenu;
  public GameObject newSavePrompt;
  public TMP_InputField newSaveInput;
  private bool newSavePromptOpen = false;

  public GridController grid;
  public GameObject saveElement;
  public GameObject savesContent;

  void Start() {
    newSaveButton.onClick.AddListener(NewSaveButtonClicked);
  }

  void Awake() {
    SavePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "saves" + Path.DirectorySeparatorChar;
    LoadSaves();
  }

  void Update() {
    if (newSavePromptOpen) {
      if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)) {
        newSavePromptOpen = false;
        newSavePrompt.SetActive(false);
        
        if (Input.GetKeyDown(KeyCode.Return)) {
          SaveToFile(newSaveInput.text);
          LoadSaves();
        }
        newSaveInput.text = "";
      }
    }
  }

  void NewSaveButtonClicked() {
    newSavePromptOpen = true;
    newSavePrompt.SetActive(true);
    newSaveInput.ActivateInputField();
  }

  void LoadSaves() {
    foreach (Transform child in savesContent.transform) {
      Destroy(child.gameObject);
    }

    DirectoryInfo savesDirecory = Directory.CreateDirectory(SavePath);
    var saves = savesDirecory.GetFiles();
    foreach (FileInfo save in saves) {
      if (save.Extension == SaveExtension) {
        string saveName = Path.GetFileNameWithoutExtension(save.FullName);
        GameObject saveObject = Instantiate(saveElement, savesContent.transform);
        saveObject.GetComponentInChildren<TMP_Text>().text = saveName;
      }
    }
  }

  void SaveToFile(string name) {
    string json = JsonUtility.ToJson(new SerializedGrid(grid));
    StreamWriter writer = new StreamWriter(Path.Combine(SavePath, name + SaveExtension));
    writer.Write(json);
    writer.Close();
  }

  SerializedGrid LoadFromFile(string name) {
    StreamReader reader = new StreamReader(Path.Combine(SavePath, name + SaveExtension));
    string json = reader.ReadToEnd();
    SerializedGrid save = JsonUtility.FromJson<SerializedGrid>(json);
    reader.Close();

    return save;
  }
}
