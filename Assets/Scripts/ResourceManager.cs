using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    
    public const int baseProductionRate = 5;
    public const int intialGold = 25;


    public Resource gold;
    public int goldPerSecond;    

    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.Find("Gold").GetComponent<Resource>();
        gold.set(intialGold);

        InvokeRepeating("UpdateGold", 0f, 1f);

    }

    // Update is called once per frame
    void UpdateGold()
    {
            getGoldPerSecond();
            updateGold();
    }

    void getGoldPerSecond(){
        goldPerSecond = baseProductionRate;
        int buildingProductionRate = 0;

         ProductionBuilding[] productionBuildings = FindObjectsOfType(typeof(ProductionBuilding)) as ProductionBuilding[];
        foreach (ProductionBuilding pb in productionBuildings)
        {
            buildingProductionRate += pb.productionRate;   
        }
        Debug.Log(buildingProductionRate);
        goldPerSecond = baseProductionRate + buildingProductionRate;

    }

    void updateGold(){
        gold.amount = gold.amount + goldPerSecond;
    }
}
