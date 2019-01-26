using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This clss shouldn't be used yet. The player is a ghost, shouldn't be a target 
// in the game yet, used for testing purposes
public class PlayerBuilding : MonoBehaviour, IBuilding
{
  float life = 20;

  public void Injure(float power)
  {
    life -= power;
    Debug.Log("hurt me more!: " + life);

    if (life <= 0)
    {
      Destroy(gameObject);
    }
  }
}