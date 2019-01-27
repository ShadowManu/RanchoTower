using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeCollision : MonoBehaviour
{
  TowerAttackBehavior towerAttackBehavior;

  void Start()
  {
    towerAttackBehavior = GetComponentInParent<TowerAttackBehavior>();
  }

  void OnTriggerEnter(Collider other)
  {
    var gameObject = other.gameObject;
    var tag = gameObject.tag;

    if (tag == "Enemy") towerAttackBehavior.EnemyReached(gameObject);
  }
}