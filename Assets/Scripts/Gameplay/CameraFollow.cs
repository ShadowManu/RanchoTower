using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  public Vector3 offset = new Vector3(-10, 5, -10);

  public float targetZoom = 5;
  public float zoomStepMultiplier = 20;
  public float minTargetZoom = 3;
  public float maxTargetZoom = 15;

  private float zoomVelocity = 0;

  public GameObject target;
  Camera cam;

  // Start is called before the first frame update
  void Start()
  {
    cam = GetComponent<Camera>();
    cam.orthographicSize = targetZoom;

    if (target == null)
    {
      var player = GameObject.Find("Player");

      if (player != null) target = player;
      else Debug.Log("OrtoCamera does not have a valid target or automatic player");
    }
  }

  // Update is called once per frame
  void Update()
  {
    detectAndApplyZoom();

    transform.position = target.transform.position + offset;
    transform.LookAt(target.transform.position);
  }

  void detectAndApplyZoom()
  {
    var axis = Input.GetAxis("Mouse ScrollWheel");

    if (axis != 0)
    {
      var desiredTargetZoom = targetZoom + (-axis) * zoomStepMultiplier;
      targetZoom = Mathf.Clamp(desiredTargetZoom, minTargetZoom, maxTargetZoom);
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
