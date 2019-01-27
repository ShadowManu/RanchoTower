using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
  Resource,
  Enemies
}

public class StatViewer : MonoBehaviour
{
  public StatType statType;
  private Text widget;

  void Start()
  {
    widget = GetComponent<Text>();
    updateStat();
    registerEvents();
  }

  void registerEvents()
  {
    switch (statType)
    {
      case StatType.Resource:
        ResourceState.instance.AmountChange += updateStat;
        break;

      case StatType.Enemies:
        EnemiesState.CurrentEnemiesChange += updateStat;
        break;
    }
    ResourceState.instance.AmountChange += updateStat;
  }

  void updateStat()
  {
    widget.text = getStat();
  }

  string getStat()
  {
    switch (statType)
    {
      case StatType.Resource:
        var amount = ResourceState.instance.amount;
        return amount.ToString();

      case StatType.Enemies:
        var currentEnemies = EnemiesState.instance.currentEnemies;
        return currentEnemies.ToString();

      default:
        return "[No StatType selected]";
    }
  }
}