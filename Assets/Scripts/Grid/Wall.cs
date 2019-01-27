using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IGridObject
{
  private readonly bool[,] shape = new bool[,] { { true } };
  public int gridPosX;
  public int gridPosY;

  public float price;
  public float Price() { return price; }

  public void SetGridPosition(int x, int y)
  {
    this.gridPosX = x;
    this.gridPosY = y;
  }

  public Tuple<bool[,], Tuple<int, int>> Space()
  {
    return new Tuple<bool[,], Tuple<int, int>>(shape, new Tuple<int, int>(shape.GetLength(0), shape.GetLength(1)));
  }

  public Tuple<int, int> GridPosition()
  {
    return new Tuple<int, int>(gridPosX, gridPosY);
  }

  public void kill()
  {
    Destroy(transform.parent.gameObject);
  }

  public string Tag()
  {
    return this.gameObject.tag;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
