using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject MobPrefab; // Prefab to spawn
  public GameObject EnemyTarget;

  public float initialCooldown;
  public float waveCooldown;
  public int waveAmount;

  void Start()
  {
    StartCoroutine("RunWaves");
  }

  IEnumerator RunWaves()
  {
    // Initial wait
    yield return new WaitForSeconds(initialCooldown);

    while (true)
    {
      // Spawn each mob with some delay
      for (var i = 0; i < waveAmount; i++)
      {
        var mob = Instantiate(MobPrefab, transform.position, transform.rotation);
        mob.GetComponent<EnemyBehavior>().target = EnemyTarget;

        mob.GetComponent<Rigidbody>().AddForce(randomVector(), ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
      }

      // Wait between waves
      yield return new WaitForSeconds(waveCooldown);
    }
  }

  Vector3 randomVector()
  {
    var MIN_FORCE = 5;
    var MAX_FORCE = 10;

    var direction = Vector3.ProjectOnPlane(Random.insideUnitCircle.normalized, Vector3.up);
    return direction * Random.Range(MIN_FORCE, MAX_FORCE);
  }
}
