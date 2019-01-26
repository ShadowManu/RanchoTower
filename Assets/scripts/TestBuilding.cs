using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestBuilding : MonoBehaviour, IGridObject
{
    private readonly bool[,] shape = new bool[,] { { true, true, true }, { true, true, false }, { true, false, false } };
    public int gridPosX;
    public int gridPosY;

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
        Destroy(this);
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
