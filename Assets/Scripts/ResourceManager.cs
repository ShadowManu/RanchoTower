using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    
    public Resource gold;
    public int goldPerSecond;

    float timeInterval = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.Find("Gold").GetComponent<Resource>();
        gold.set(25);

        InvokeRepeating("UpdateGold", 0f, 1f);

    }

    // Update is called once per frame
    void UpdateGold()
    {
            getGoldPerSecond();
            updateGold();
    }

    void getGoldPerSecond(){
        goldPerSecond = 1;
    }

    void updateGold(){
        gold.amount = gold.amount + goldPerSecond;
    }
}
