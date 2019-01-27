using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyMode
{
  Seek, // Chasing a target
  Attack // Doing damage to target
}

[RequireComponent(typeof(Rigidbody))]
public class EnemyBehavior : MonoBehaviour
{
  public float speed = 0.1f;
  public float power = 5.0f;
  public int period = 2;

  public GameObject target;

  private HPBehaviour hPBehaviour;
  private Rigidbody rb;
  private EnemyMode mode = EnemyMode.Seek;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (mode == EnemyMode.Seek && target != null) MoveTowardsTarget();
  }

  void onDestroy()
  {
    CancelInvoke("AttackCycle");
  }

  void MoveTowardsTarget()
  {
    var targetPosition = target.transform.position;
    var ownPosition = transform.position;

    var difference = Vector3.ProjectOnPlane((targetPosition - ownPosition), Vector3.up);

    // Collision code must block the movement, no checks being done here for being already close

    var offset = difference.normalized * speed;
    rb.MovePosition(transform.position + offset);
  }

  void OnCollisionEnter(Collision collision)
  {
    // on collision, means the enemy just reached an enemy it will have to attack
    // so, start its attacking cycle, until the collided building is dead or the enemy itself adies
    // (there's an extra condition in the future when the target gets far away somehow)


    var gameObject = collision.collider.gameObject;

    // Discard collision if not a building (or player, for testing) or already attacking
    var tag = gameObject.tag;
    if (tag != "Building" || mode == EnemyMode.Attack) return;

    mode = EnemyMode.Attack;
    hPBehaviour = gameObject.GetComponent<HPBehaviour>();

    InvokeRepeating("AttackCycle", 0, period);
  }

  void AttackCycle()
  {
    // Attack if possible
    if (CanAttack()) hPBehaviour.decreaseHP(power);

    // Finish attacking if so
    if (!CanAttack())
    {
      CancelInvoke("AttackCycle");
      mode = EnemyMode.Seek;
    }
  }

  private bool CanAttack()
  {
    return hPBehaviour != null && hPBehaviour.isAlive();
  }
}
