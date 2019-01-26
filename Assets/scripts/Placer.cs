using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private GameObject Prefab;
    public Grid Grid;

    public Material CanPlace;

    public Material NoPlace;



    private GameObject intancedO;

    private Material instancedOriginalM;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPrefab(GameObject _Prefab){ 
        this.Prefab = _Prefab;
        intancedO = Instantiate(Prefab);
        instancedOriginalM = intancedO.transform.GetChild(0).GetComponent<MeshRenderer>().material;
     }

    // Update is called once per frame
    void Update()
    {
        if (Prefab != null)
        {
            var pos = Grid.CurrentCellPosition();
            var pos2 = Grid.CellPosition(pos.Item1.Item1, pos.Item1.Item2);
            intancedO.transform.position = new Vector3(pos2.x, 0, pos2.y);
            var igrid = (IGridObject)intancedO.GetComponentInChildren(typeof(IGridObject));
            if (Grid.CanAdd(igrid, pos.Item1.Item1, pos.Item1.Item2))
            {
                intancedO.transform.GetChild(0).GetComponent<MeshRenderer>().material = CanPlace;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Grid.Put(igrid, pos.Item1.Item1, pos.Item1.Item2);
                    igrid.SetGridPosition(pos.Item1.Item1, pos.Item1.Item2);
                    intancedO.transform.GetChild(0).GetComponent<MeshRenderer>().material = instancedOriginalM;
                    Prefab = null;
                }
            }
            else
            {
                intancedO.transform.GetChild(0).GetComponent<MeshRenderer>().material = NoPlace;
            }
        }
    }
}
