using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerControllerUI : MonoBehaviour {
  public GridController grid;
  public RectTransform layerRect;
  public int size = 100;

  void Start() {
    layerRect.sizeDelta = new Vector2(size, size / grid.layers);
  }

  void Propagate() {
    layerRect.localPosition = new Vector2(size / 2, size / grid.layers / 2 + size / grid.layers * grid.player.selectedLayer);
  }
}
