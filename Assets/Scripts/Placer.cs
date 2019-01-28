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

    private Tuple<bool,int> SetPrefabPromise;

    // Start is called before the first frame update
    void Start()
    {
        SetPrefabPromise = new Tuple<bool, int>(false, 0);
    }

    public void SetPrefab(int index)
    {
        SetPrefabPromise = new Tuple<bool, int>(true, index);
    }

    public void unsetPrefab()
    {
        Destroy(intancedO);
        this.Prefab = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (SetPrefabPromise.Item1) {
            if (intancedO != null) this.unsetPrefab();
            this.Prefab = Prefabs[SetPrefabPromise.Item2];
            intancedO = Instantiate(PrefabsShadows[SetPrefabPromise.Item2]);
            SetPrefabPromise = new Tuple<bool, int>(false, 0);
        }
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
                var meshrenderers = intancedO.GetComponentsInChildren<Renderer>();
                foreach (Renderer mr in meshrenderers) mr.material = CanPlace;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    var temp = Instantiate(this.Prefab);
                    temp.transform.position = new Vector3(pos2.x, 0, pos2.y);
                    var tempIgrid = (IGridObject)temp.GetComponentInChildren(typeof(IGridObject));
                    tempIgrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
                    Grid.Put(tempIgrid, pos.Item1.Item1, pos.Item1.Item2);
                    igrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
                    Prefab = null;
                    Destroy(intancedO);
                }
            }
            else
            {
                var meshrenderers = intancedO.GetComponentsInChildren<Renderer>();
                foreach (Renderer mr in meshrenderers) mr.material = NoPlace;
            }
        }
    }
}
