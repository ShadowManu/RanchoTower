using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Vector3 offset = new Vector3(-10, 5, -10);

  public float zoomMultiplier = 20;
  float targetZoom = 9;
  float zoomVelocity = 0;

  public GameObject player;
  Camera cam;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
    cam.nearClipPlane = -100;
  }

  // Update is called once per frame
  void Update()
  {
    detectAndApplyZoom();

    transform.position = player.transform.position + offset;
    transform.LookAt(player.transform.position);
  }

  void detectAndApplyZoom()
  {
    var axis = Input.GetAxis("Mouse ScrollWheel");

    if (axis != 0)
    {
      var desiredTargetZoom = targetZoom + (-axis) * zoomMultiplier;
      targetZoom = Mathf.Clamp(desiredTargetZoom, 5, 13);
    }

    // Zoom if needed
    if (cam.orthographicSize != targetZoom)
    {
      cam.orthographicSize = Mathf.SmoothDamp(
        cam.orthographicSize,
        targetZoom,
        ref zoomVelocity,
        0.25f
      );
    }
  }
}
