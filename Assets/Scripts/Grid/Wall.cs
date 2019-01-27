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
        for (int i = 0; i < 6; i++) transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        bool top = Grid.instance.CellTag(gridPosX, gridPosY + 1) == "Building";
        bool down = Grid.instance.CellTag(gridPosX, gridPosY - 1) == "Building";
        bool left = Grid.instance.CellTag(gridPosX - 1, gridPosY) == "Building";
        bool right = Grid.instance.CellTag(gridPosX + 1, gridPosY) == "Building";

        if (top && down && left && right)
        {
            transform.parent.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 0;
            return;
        }

        if (top && down && left)
        {
            transform.parent.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 0;
            return;
        }
        if (top && left && right)
        {
            transform.parent.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * -90;
            return;
        }
        if (top && down && right)
        {
            transform.parent.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 180;
            return;
        }
        if (down && left && right)
        {
            transform.parent.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 90;
            return;
        }

        if (down && top)
        {
            transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 0;
            return;
        }
        if (left && right)
        {
            transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 90;
            return;
        }

        if (top && right)
        {
            transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * -90;
            return;
        }
        if (top && left)
        {
            transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 0;
            return;
        }
        if (down && right)
        {
            transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 180;
            return;
        }
        if (down && left)
        {
            transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 90;
            return;
        }

        if (top)
        {
            transform.parent.gameObject.transform.GetChild(5).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 270;
            return;
        }
        if (down)
        {
            transform.parent.gameObject.transform.GetChild(5).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 90;
            return;
        }
        if (right)
        {
            transform.parent.gameObject.transform.GetChild(5).gameObject.SetActive(true);
            transform.parent.eulerAngles = Vector3.up * 180;
            return;
        }
        if (left)
        {
            transform.parent.gameObject.transform.GetChild(5).gameObject.SetActive(true);
            return;
        }
        transform.parent.gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }
}
