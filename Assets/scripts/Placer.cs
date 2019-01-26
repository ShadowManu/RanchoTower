using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject o;
    public Grid g;

    private GameObject intancedO;

    // Start is called before the first frame update
    void Start()
    {
        intancedO = Instantiate(o);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = g.CurrentCellPosition();
        var pos2 = g.CellPosition(pos.Item1.Item1, pos.Item1.Item2);
        Debug.Log(pos2.x + " " + pos2.y);
        intancedO.transform.position = new Vector3(pos2.x, 0, pos2.y);
    }
}
