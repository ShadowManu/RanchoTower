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
    detectAndApplyForce(KeyCode.W, Vector3.forward);
    detectAndApplyForce(KeyCode.A, Vector3.left);
    detectAndApplyForce(KeyCode.S, Vector3.back);
    detectAndApplyForce(KeyCode.D, Vector3.right);
    
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
      Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
    ) {
      anim.SetBool("walk", true);
    }
    else
    {
      anim.SetBool("walk", false);
    }
  }

  void detectAndApplyForce(KeyCode key, Vector3 direction)
  {
    if (Input.GetKey(key))
    {
      rb.AddForce(direction * multiplier);
      direction.y = transform.position.y;
      transform.DOLookAt(transform.position + direction, 0);
    }
  }
}