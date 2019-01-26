using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public int amount;

    public Resource(){
        amount = 0;
    }

    public Resource(int am){
        amount = am;
    }

    public void decrease(int am){
        if (amount < am){
            amount = 0;
        } else {
            amount = amount - am;
        }
    }

    public void increase(int am){
        amount = amount + am;
    }

    public void set(int am){
        if (am < 0){
            am = 0;
        }
        amount = am;
    }

    public void deplete(int am){
        amount = 0;
    }
    
}
