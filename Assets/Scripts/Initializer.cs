using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Helper monobehaviour to assing a house gameobject into a grid
public class Initializer : MonoBehaviour
{
  public Grid grid;
  public GameObject housePrefab;

  void Start()
  {
    var house = Instantiate(housePrefab);
    var gridObject = house.GetComponentInChildren<IGridObject>();
    gridObject.SetGridPosition(500, 500);
    grid.Put(gridObject, 500, 500);

    EnemiesState.instance.EnemyTarget = house.transform.GetChild(0).gameObject;

    Destroy(this);
  }
}
