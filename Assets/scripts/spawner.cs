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

    private int EnemyOnSpawn;

    public GameObject EnemyTarget;

    // Start is called before the first frame update
    void Start()
    {
        this.NextWaveTime = this.InitialCoolDown;
        this.ToSpawn = 0;
        this.EnemyOnSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.NextWaveTime -= Time.deltaTime;
        if (this.NextWaveTime <= 0)
        {
            this.ToSpawn += this.WaveAmount;
            this.NextWaveTime += this.WaveCooldown;
        }
        if (this.ToSpawn > 0 && this.EnemyOnSpawn == 0)
        {
            var newObj = Instantiate(Mob);
            ((EnemyBehavior)newObj.GetComponentInChildren(typeof(EnemyBehavior))).target = EnemyTarget;
            newObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.ToSpawn--;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        this.EnemyOnSpawn += col.gameObject.tag == "Enemy" ? 1 : 0;
    }
    void OnCollisionExit(Collision col)
    {
        this.EnemyOnSpawn -= col.gameObject.tag == "Enemy" ? 1 : 0;
    }
}
