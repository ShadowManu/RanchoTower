using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBehaviour : MonoBehaviour
{

  public float totalHP;
  public float currentHP;
  public GameObject[] HPBar;

  public void setHP(int t)
  {
    totalHP = t;
    currentHP = t;
  }

  public void decreaseHP(float dm)
  {
    currentHP = currentHP - dm;
    if (currentHP <= 0) Destroy(gameObject);
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
