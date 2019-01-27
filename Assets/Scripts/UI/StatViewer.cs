using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatViewer : MonoBehaviour
{
  private Text widget;

  void Start()
  {
    widget = GetComponent<Text>();
    updateStat();
    registerEvents();
  }

  void registerEvents()
  {
    ResourceState.instance.AmountChange += updateStat;
  }

  void updateStat()
  {
    widget.text = getStat();
  }

  string getStat()
  {
    var amount = ResourceState.instance.amount;
    return amount.ToString();
  }
}