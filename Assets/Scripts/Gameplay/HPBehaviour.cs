using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public delegate void EnemyKillHandler();

public class HPBehaviour : MonoBehaviour
{

  public float totalHP;
  public float currentHP;


  public CanvasGroup HPBarContainer;
  public Image HPBar;

  public static event EnemyKillHandler EnemyKillEvent;

  public void setHP(int t)
  {
    totalHP = t;
    currentHP = t;
  }

  public void decreaseHP(float dm)
  {
    if (currentHP == totalHP)
      HPBarContainer.DOFade(1, .2f);

    currentHP = currentHP - dm;
    updateHPBar(currentHP/totalHP);
    if (currentHP <= 0)
    {
      // TODO: this should be improved

      // if its an enemy, notify
      if (gameObject.tag == "Enemy")
      {
        EnemyKillEvent();
        if (GetComponent<UIMessage>())
          GetComponent<UIMessage>().Show();
        Destroy(gameObject);
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

  public void updateHPBar(float to)
  {

    HPBar.DOKill();
    HPBar.DOFillAmount(to, .2f); 

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
