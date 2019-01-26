using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject Mob;

    public float InitialCoolDown;

    public float WaveCooldown;

    public int WaveAmount;

    private float NextWaveTime;

    private int ToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        this.NextWaveTime = this.InitialCoolDown;
        this.ToSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.NextWaveTime -= Time.deltaTime;
        if (this.NextWaveTime <= 0) this.ToSpawn += this.WaveAmount;
    }

    void OnCollision()
    {

    }
}
