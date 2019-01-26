using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelector : MonoBehaviour
{
    public GameObject actualObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectPrefab(GameObject obj) {
        actualObject = obj;
        Debug.Log(obj.name);
    }
}
