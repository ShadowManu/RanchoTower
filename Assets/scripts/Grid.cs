using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    private Tuple<bool, IGridObject>[,] matrix;

    public Camera cam;

    private readonly Plane plane = new Plane(Vector3.up, Vector3.zero);

    // Start is called before the first frame update
    void Start()
    {
        this.matrix = new Tuple<bool, IGridObject>[sizeX, sizeY];
        for (int i = 0; i != this.sizeX; i++)
            for (int j = 0; j != this.sizeY; j++)
                this.matrix[i, j] = new Tuple<bool, IGridObject>(false, null);
    }

    public bool CanAdd(IGridObject o, int x, int y)
    {
        var space = o.Space();
        bool result = true;

        for (int i = x; i != x + space.Item2.Item1 && result; i++)
            for (int j = y; j != y + space.Item2.Item2 && result; j++)
                result = !(space.Item1[i - x, j - y] && this.matrix[i, j].Item1);

        return result;
    }

    public void Remove(IGridObject o)
    {
        var position = o.GridPosition();
        var space = o.Space();
        int x = position.Item1; int y = position.Item2;

        for (int i = x; i != x + space.Item2.Item1; i++)
            for (int j = y; j != y + space.Item2.Item2; j++)
                if (space.Item1[i - x, j - y] && this.matrix[i, j].Item1)
                    this.matrix[i, j] = new Tuple<bool, IGridObject>(false, null);
        o.kill();
    }

    public void Put(IGridObject o, int x, int y)
    {
        var space = o.Space();

        for (int i = x; i != x + space.Item2.Item1; i++)
            for (int j = y; j != y + space.Item2.Item2; j++)
                if (space.Item1[i - x, j - y])
                {
                    if (this.matrix[i, j].Item2 != null) this.Remove(this.matrix[i, j].Item2);
                    this.matrix[i, j] = new Tuple<bool, IGridObject>(true, o);
                }
    }

    public List<IGridObject> Conflict(IGridObject o, int x, int y)
    {
        var space = o.Space();
        var dic = new Dictionary<Tuple<int, int>, IGridObject>();

        for (int i = x; i != x + space.Item2.Item1; i++)
            for (int j = y; j != y + space.Item2.Item2; j++)
                if (space.Item1[i - x, j - y] && this.matrix[i, j].Item1 && !dic.ContainsKey(this.matrix[i, j].Item2.GridPosition()))
                    dic.Add(this.matrix[i, j].Item2.GridPosition(), this.matrix[i, j].Item2);

        var result = new List<IGridObject>();
        foreach (var i in dic) result.Add(i.Value);
        return result;
    }

    // coordinates of center of a cell
    public Vector2 CellPosition(int x, int y)
    {
        float cellSizeX = this.gameObject.transform.localScale.x * 10 / this.sizeX;
        float cellSizeY = this.gameObject.transform.localScale.y * 10 / this.sizeY;

        Vector2 topLeftCorner = new Vector2(
            (this.gameObject.transform.position.x - 5) * this.gameObject.transform.localScale.x,
            (this.gameObject.transform.position.y + 5) * this.gameObject.transform.localScale.y);

        return new Vector2(topLeftCorner.x + cellSizeX / 2 + x * cellSizeX,
            topLeftCorner.y - cellSizeY / 2 - y * cellSizeY);
    }

    public Tuple<int, int> CellFromPosition(Vector3 vec)
    {
        float cellSizeX = this.gameObject.transform.localScale.x * 10 / this.sizeX;
        float cellSizeY = this.gameObject.transform.localScale.y * 10 / this.sizeY;

        Vector2 topLeftCorner = new Vector2(
            (this.gameObject.transform.position.x - 5) * this.gameObject.transform.localScale.x,
            (this.gameObject.transform.position.y + 5) * this.gameObject.transform.localScale.y);

        var cellX = Mathf.FloorToInt((vec.x - topLeftCorner.x) / cellSizeX);
        var celly = Mathf.FloorToInt((topLeftCorner.y - vec.z) / cellSizeY);
        return new Tuple<int, int>(cellX, celly);
    }

    public Tuple<Tuple<int, int>, Vector3> CurrentCellPosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (plane.Raycast(ray, out enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            return new Tuple<Tuple<int, int>, Vector3>(this.CellFromPosition(hitPoint), hitPoint);
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
