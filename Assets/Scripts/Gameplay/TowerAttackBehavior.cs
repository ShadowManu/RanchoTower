using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;

enum TowerMode
{
  Watch,
  Attack
}

public class TowerAttackBehavior : MonoBehaviour
{
  public float power = 5.0f;
  public float period = 0.5f;
  public GameObject arrow;
  public Transform spawnPoint;

  CapsuleCollider rangeTrigger;
  GameObject target;
  HPBehaviour hPBehavior;

  TowerMode mode = TowerMode.Watch;


  
  void arrowAnimation()
  {
    if (target){ 
      Transform enemy = target.transform;
      GameObject a = Instantiate(arrow, spawnPoint.transform.position, Quaternion.identity);
      ConstraintSource c = new ConstraintSource();
      c.sourceTransform = enemy;
      c.weight = 1;
      a.GetComponent<LookAtConstraint>().SetSource(0, c);
      a.transform.DOMoveX(enemy.position.x, 0.5f);
      a.transform.DOMoveZ(enemy.position.z, 0.5f);
      a.transform.DOMoveY(enemy.position.y, 0.5f).SetEase(Ease.InQuad);
      Destroy(a, 1f);
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    rangeTrigger = GetComponentInChildren<CapsuleCollider>();
  }

  public void EnemyReached(GameObject gameObject)
  {
    // If already attacking, ignore new enemy
    if (mode == TowerMode.Attack) return;

    mode = TowerMode.Attack;
    target = gameObject;
    hPBehavior = gameObject.GetComponent<HPBehaviour>();

    arrowAnimation();
    InvokeRepeating("AttackCycle", period, period);
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
      hPBehavior.decreaseHP(power);
    }
    arrowAnimation();
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
      if (collider.gameObject.tag != "Enemy") continue;

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
