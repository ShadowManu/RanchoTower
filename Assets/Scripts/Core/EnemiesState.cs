using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Welcome (after)
// Repeat this:
// Wave timer
// Spawn all enemies
// After last is dead, reboot

public class EnemiesState : MonoBehaviour
{
  public static EnemiesState instance = null;
  public static event EmptyHandler CurrentEnemiesChange;
  public static event EmptyHandler CurrentWavesChange;

  public GameObject MobPrefab;
  public GameObject EnemyTarget;
  public float wavePrepTime = 10f;
  public float waveEndTime = 1f;

  public int nWaves = 3;
  public int nEnemies = 5;

  public int currentWave = 0;
  public int currentEnemies = 0;

  private List<SpawnerPoint> spawners;

  void Awake()
  {
    // Singleton code
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    spawners = new List<SpawnerPoint>();
    foreach (var item in GetComponentsInChildren<SpawnerPoint>())
      spawners.Add(item);

    // Apply target to prefab (and future instances)
    MobPrefab.GetComponent<EnemyBehavior>().target = EnemyTarget;

    HPBehaviour.EnemyKillEvent += OnEnemyKilled;
    StartWave();
  }

  // Wave Lifecycle methods

  void StartWave()
  {
    currentWave += 1;
    currentEnemies = nEnemies;
    notifyCurrentEnemies();
    StartCoroutine(DelayedSpawn());
  }

  IEnumerator DelayedSpawn()
  {
    yield return new WaitForSeconds(wavePrepTime);
    SpawnWaveEnemies();
  }

  void OnEnemyKilled()
  {
    currentEnemies -= 1;
    notifyCurrentEnemies();
    if (currentEnemies == 0)
      StartCoroutine(EndWave());
  }

  IEnumerator EndWave()
  {
    // we can do something short additionally here
    yield return new WaitForSeconds(waveEndTime);
    StartWave();
  }

  // LONG METHODS BELOW

  void SpawnWaveEnemies()
  {
    // TODO IMPROVE ALGORITHM
    // spawn points can be empty and some other really overloaded
    var nums = new int[spawners.Count];
    for (int i = 0; i < nums.Length; i++) nums[i] = i;
    Algorithms.reshuffle(nums);

    int mobs = nEnemies;
    for (int i = 0; i < nums.Length; i++)
    {
      int j = Random.Range(0, mobs);
      spawners[nums[i]].RunSpawn(MobPrefab, j);
      mobs -= j;
    }
  }

  private void notifyCurrentEnemies()
  {
    if (CurrentEnemiesChange != null) CurrentEnemiesChange();
  }
}
