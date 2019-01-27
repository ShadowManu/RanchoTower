using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnServer : MonoBehaviour
{
    public GameObject MobPrefab;

    public GameObject EnemyTarget;

    public List<Spawner> spawners;

    public int WaveAmount;

    public int WaveCooldown;

    public int InitialCooldown;

    // Start is called before the first frame update
    void Start()
    {
        MobPrefab.GetComponent<EnemyBehavior>().target = EnemyTarget;
        StartCoroutine("RunWaves");
    }

    IEnumerator RunWaves()
    {
        // Initial wait
        yield return new WaitForSeconds(InitialCooldown);

        while (true)
        {
            var nums = new int[spawners.Count];
            for (int i = 0; i < nums.Length; i++) nums[i] = i;
            reshuffle(nums);

            int mobs = WaveAmount;
            for (int i = 0; i < nums.Length; i++)
            {
                int j = Random.Range(0, mobs);
                spawners[nums[i]].RunSpawn(MobPrefab, j);
                mobs -= j;
            }

            // Wait between waves
            yield return new WaitForSeconds(WaveCooldown);
        }
    }

    void reshuffle(int[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            var tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }
}
