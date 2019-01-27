﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject Instantiator;

    private int amount;
    void Start()
    {
    }

    public void RunSpawn(GameObject o, int i)
    {
        Instantiator = o;
        amount = i;
        StartCoroutine("RunSpawnCo");
    }

    IEnumerator RunSpawnCo()
    {
        for (var i = 0; i < amount; i++)
        {
            var mob = Instantiate(Instantiator, transform.position, transform.rotation);

            mob.GetComponent<Rigidbody>().AddForce(randomVector(), ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
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
