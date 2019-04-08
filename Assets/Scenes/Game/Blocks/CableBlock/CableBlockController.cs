using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableBlockController : PropagatingBlockController {
  public Sprite sprite;
  public Sprite chargedSprite;

  public override void init(BlockPosition position) {
    base.init(position);
    type = BlockType.Cable;
    spriteRenderer.sprite = sprite;
  }

  protected override void setCharge(bool newCharge) {
    if (charge != newCharge) {
      if (newCharge) spriteRenderer.sprite = chargedSprite;
      else spriteRenderer.sprite = sprite;
      charge = newCharge;
    }
  }
}
