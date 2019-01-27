﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void EnemyKillHandler();

public class HPBehaviour : MonoBehaviour
{

  public float totalHP;
  public float currentHP;
  public GameObject[] HPBar;

  public static event EnemyKillHandler EnemyKillEvent;

  public void setHP(int t)
  {
    totalHP = t;
    currentHP = t;
  }

  public void decreaseHP(float dm)
  {
    currentHP = currentHP - dm;
    if (currentHP <= 0)
    {
      // TODO: this should be improved

      // if its an enemy, notify
      if (gameObject.tag == "Enemy")
      {
        EnemyKillEvent();
        Destroy(this);
      }
      else
      {
        Grid.instance.Remove(GetComponent<IGridObject>());
      }
    }
  }

  public bool isAlive()
  {
    return currentHP > 0;
  }

  void checkHP()
  {
    Debug.Log(currentHP);
  }


  public int getHPbarIndex()
  {
    float HPdivision = (float)totalHP / 10f;
    int index = (int)Mathf.Floor((float)currentHP / HPdivision);
    if (index < 0)
    {
      index = 1;
    }
    return index;
  }

  public void updateHP()
  {

    //Debug.Log(pb.productionRate);
    int barTotal = transform.GetChild(0).GetChild(0).childCount;

    int barIndex = getHPbarIndex();

    Debug.Log(barIndex);

    for (int i = 0; i < barIndex; i++)
    {
      HPBar[i].GetComponent<Image>().color = Color.white;
    }

    for (int i = barIndex; i < barTotal; i++)
    {
      HPBar[i].GetComponent<Image>().color = Color.black;
    }

  }

  // Start is called before the first frame update
  void Start()
  {

    // updateHP();
  }

  // Update is called once per frame
  void Update()
  {
    // updateHP();
  }
}
