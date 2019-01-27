using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private GameObject Prefab;

    public Grid Grid;

    public Material CanPlace;

    public Material NoPlace;

    public GameObject[] Prefabs;

    public GameObject[] PrefabsShadows;

    private GameObject intancedO;

    private Material instancedOriginalM;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPrefab(int index)
    {
        this.Prefab = Prefabs[index];
        intancedO = Instantiate(PrefabsShadows[index]);
        instancedOriginalM = intancedO.transform.GetChild(0)
            .GetComponent<MeshRenderer>().material;
    }

    public void unsetPrefab()
    {
        Destroy(intancedO);
        this.Prefab = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.unsetPrefab();
        }
        if (Prefab != null)
        {
            var pos = Grid.CurrentCellPosition();
            var pos2 = Grid.CellPosition(pos.Item1.Item1, pos.Item1.Item2);
            intancedO.transform.position = new Vector3(pos2.x, 0, pos2.y);
            var igrid = (IGridObject)intancedO.GetComponentInChildren(typeof(IGridObject));
            igrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
            if (Grid.CanAdd(igrid, pos.Item1.Item1, pos.Item1.Item2))
            {
                var meshrenderers = intancedO.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in meshrenderers) mr.material = CanPlace;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    var temp = Instantiate(this.Prefab);
                    temp.transform.position = new Vector3(pos2.x, 0, pos2.y);
                    var tempIgrid = (IGridObject)temp.GetComponentInChildren(typeof(IGridObject));
                    tempIgrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
                    Grid.Put(tempIgrid, pos.Item1.Item1, pos.Item1.Item2);
                    igrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
                    intancedO.transform.GetChild(0).GetComponent<MeshRenderer>().material = instancedOriginalM;
                    Prefab = null;
                    Destroy(intancedO);
                }
            }
            else
            {
                var meshrenderers = intancedO.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in meshrenderers) mr.material = NoPlace;
            }
        }
    }
}
