using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuilding : MonoBehaviour
{

    public int productionRate;
    //public int totalHP;
   // public int currentHP;

    public HPBehaviour HP; 

    public ProductionBuilding(int pr){
        productionRate = pr;
    }

    void Start(){

        HP = GetComponent<HPBehaviour>();
        HP.setHP(50);

        HP.currentHP = 12;
    }


}
