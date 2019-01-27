using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializer : MonoBehaviour
{
    public Grid grid;
    public GameObject house;
    // Start is called before the first frame update
    void Start()
    {
        grid.Put((IGridObject)Instantiate(this.house).GetComponentInChildren(typeof(IGridObject)),500,500);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
