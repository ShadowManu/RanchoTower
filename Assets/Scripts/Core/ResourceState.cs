using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceState : MonoBehaviour
{
  public static ResourceState instance = null;
  public event EmptyHandler AmountChange;

  public float amount = 25f;
  public float localRate = 5f;


  void Awake()
  {
    // Singleton code
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    InvokeRepeating("UpdateGold", 1f, 4f);
  }

  public void Spend(float am)
  {
    amount -= am;
    notifyAmountChange();
  }

  public bool CanSpend(float am)
  {
    return amount >= am;
  }

  private void UpdateGold()
  {
    var rate = GlobalRate();
    amount += rate;
    notifyAmountChange();
  }

  private float GlobalRate()
  {
    var buildings = FindObjectsOfType<ProductionBuilding>();
    var extra = 0f;

    foreach (var building in buildings)
      extra += building.productionRate;

    return localRate + extra;
  }

  private void notifyAmountChange()
  {
    if (AmountChange != null) AmountChange();
  }
}