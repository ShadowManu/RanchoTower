using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
  float multiplier = 50;
  Rigidbody rb;
  Transform transform;
  static Animator anim;


  void Start()
  {

    rb = GetComponent<Rigidbody>();
    transform = GetComponent<Transform>();
    rb.drag = 5;
    anim = GetComponentInChildren<Animator>();
  }

  void Update()
  {
    // When the user clicks WASD, player moves physics-based in the given direction
    var inputDirection = InputDirection();

    // Valid movement
    if (inputDirection != Vector3.zero)
    {
      rb.AddForce(inputDirection * multiplier);
      transform.DOLookAt(transform.position + inputDirection, 0);
      anim.SetBool("walk", true);
    }
    else
    {
      anim.SetBool("walk", false);
    }
  }

  private Vector3 InputDirection()
  {
    var direction = Vector3.zero;

    if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
    if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
    if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
    if (Input.GetKey(KeyCode.D)) direction += Vector3.right;

    var normalized = direction.normalized;
    var relativeToCamera = Camera.main.transform.TransformDirection(normalized);
    var flat = Vector3.ProjectOnPlane(relativeToCamera, Vector3.up);

    return flat;
  }
}