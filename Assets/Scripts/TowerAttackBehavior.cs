using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TowerMode
{
  Watch,
  Attack
}

public class TowerAttackBehavior : MonoBehaviour
{
  public float power = 5.0f;

  CapsuleCollider rangeTrigger;
  GameObject target;
  HPBehaviour hPBehavior;

  TowerMode mode = TowerMode.Watch;

  // Start is called before the first frame update
  void Start()
  {
    rangeTrigger = GetComponentInChildren<CapsuleCollider>();
  }

  void EnemyReached(GameObject gameObject)
  {
    // If already attacking, ignore new enemy
    if (mode == TowerMode.Attack) return;

    mode = TowerMode.Attack;
    target = gameObject;
    hPBehavior = gameObject.GetComponent<HPBehaviour>();

    var ATTACK_PERIOD = 1;
    InvokeRepeating("AttackCycle", 0, ATTACK_PERIOD);
  }

  void AttackCycle()
  {
    // Enemy is dead
    if (hPBehavior == null)
    {
      CancelInvoke("AttackCycle");
      mode = TowerMode.Watch;

      // Check if we can keep attacking
      var closeEnemy = FindCloseEnemy();
      if (closeEnemy) EnemyReached(closeEnemy);
      return;

      // Enemy can be attacked
    }
    else
    {
      hPBehavior.decreaseHP((int)power);
    }
  }

  GameObject FindCloseEnemy()
  {
    var center = gameObject.transform.position;
    var radius = rangeTrigger.radius;

    var colliders = Physics.OverlapSphere(center, radius);
    var bestDistance = Mathf.Infinity;
    GameObject bestEnemy = null;

    foreach (var collider in colliders)
    {
      if (collider.gameObject.tag != "Enemy") return null;

      var distance = (collider.gameObject.transform.position - transform.position).magnitude;
      if (distance < bestDistance)
      {
        bestDistance = distance;
        bestEnemy = collider.gameObject;
      }
    }

    return bestEnemy;
  }
}
