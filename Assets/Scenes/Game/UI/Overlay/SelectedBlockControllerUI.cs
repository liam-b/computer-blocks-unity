using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedBlockControllerUI : MonoBehaviour {
  public GridController grid;
  public Image image;

  public Sprite cableBlockSprite;
  public Sprite sourceBlockSprite;
  public Sprite inverterBlockSprite;
  public Sprite delayBlockSprite;
  public Sprite viaBlockSprite;

  void Update() {
    if (grid.player.selectedBlockType == BlockType.Cable) image.sprite = cableBlockSprite;
    if (grid.player.selectedBlockType == BlockType.Source) image.sprite = sourceBlockSprite;
    if (grid.player.selectedBlockType == BlockType.Inverter) image.sprite = inverterBlockSprite;
    if (grid.player.selectedBlockType == BlockType.Delay) image.sprite = delayBlockSprite;
    if (grid.player.selectedBlockType == BlockType.Via) image.sprite = viaBlockSprite;

    image.rectTransform.eulerAngles = Vector3.forward * 90 * -grid.player.selectedRotation;
  }
}
