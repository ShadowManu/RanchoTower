using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceState : MonoBehaviour
{
  public static ResourceState instance = null;
  public static event EmptyHandler AmountChange;

  public float initialAmount = 25f;
  public float localRate = 5f;

  private float amount;

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
    amount = initialAmount;
    InvokeRepeating("UpdateGold", 1f, 1f);
  }

  public void Spend(float amount)
  {
    amount -= amount;
    AmountChange();
  }

  private void UpdateGold()
  {
    var rate = GlobalRate();
    amount += rate;
    AmountChange();
  }

  private float GlobalRate()
  {
    var buildings = FindObjectsOfType<ProductionBuilding>();
    var extra = 0f;

    foreach (var building in buildings)
      extra += building.productionRate;

    return extra;
  }
}