using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Helper monobehaviour to assing a house gameobject into a grid
public class KillCountManager : MonoBehaviour
{
  public int killCount = 0;
  public Text killCountText;

  void Start()
  {
    HPBehaviour.EnemyKillEvent += OnEnemyKill;
  }

  public void UpdateText()
  {
    killCountText.text = killCount.ToString();
  }

  public void IncreaseKillCount()
  {
    killCount += 1;
  }

  public void OnEnemyKill()
  {
    IncreaseKillCount();
    UpdateText();
  }

}