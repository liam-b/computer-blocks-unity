using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
  public float scrollSensitivity;
  public float movementSensitivity;
  public float minScroll;
  public float maxScroll;
  public float scaleExponent;

  public PauseMenuController pauseMenu;

  void FixedUpdate() {
    if (!pauseMenu.open) {
      float scale = Mathf.Pow(Camera.main.orthographicSize, scaleExponent);

      float scroll = Camera.main.orthographicSize + Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * scale;
      Camera.main.orthographicSize = Mathf.Clamp(scroll, minScroll, maxScroll);

      if (Input.GetKey(KeyCode.W)) transform.Translate(Vector3.up * movementSensitivity * scale);
      if (Input.GetKey(KeyCode.S)) transform.Translate(Vector3.down * movementSensitivity * scale);
      if (Input.GetKey(KeyCode.A)) transform.Translate(Vector3.left * movementSensitivity * scale);
      if (Input.GetKey(KeyCode.D)) transform.Translate(Vector3.right * movementSensitivity * scale);
    }
  }
}
