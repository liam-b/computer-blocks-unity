using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLineController : MonoBehaviour {
  private LineRenderer lineRenderer;

  void Awake() {
    lineRenderer = GetComponent<LineRenderer>();
  }

  public void SetLine(Direction direction, Vector2 position, int distance, float width) {
    Vector2 offset;
    if (direction == Direction.Vertical) {
      offset = Vector2.up * distance;
    } else {
      offset = Vector2.right * distance;
    }

    lineRenderer.startWidth = width;
    lineRenderer.endWidth = width;

    position = new Vector2(position.x + width / 2, position.y + width / 2);
    transform.Translate(position);
    lineRenderer.SetPositions(new Vector3[]{offset, -offset});
  }
}
