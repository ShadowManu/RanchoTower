using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
  float multiplier = 50;
  Rigidbody rb;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    rb.drag = 5;
  }

  void Update()
  {
    // When the user clicks WASD, player moves physics-based in the given direction
    detectAndApplyForce(KeyCode.W, Vector3.forward);
    detectAndApplyForce(KeyCode.A, Vector3.left);
    detectAndApplyForce(KeyCode.S, Vector3.back);
    detectAndApplyForce(KeyCode.D, Vector3.right);
  }

  void detectAndApplyForce(KeyCode key, Vector3 direction)
  {
    if (Input.GetKey(key))
    {
      rb.AddForce(direction * multiplier);
    }
  }
}