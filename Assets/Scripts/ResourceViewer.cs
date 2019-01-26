using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour{ 

    public Text amountText;

    // Start is called before the first frame update
    void Start()
    {
        //SetAmountText(0);
        amountText = GameObject.Find("ResourceText").GetComponent<Text>();
        //gold = GameObject.Find("Gold").GetComponent<Resource>();
        //Debug.Log(gold.amount);
    }

    public void SetAmountText(int amount){
        amountText.text = amount.ToString ();
    }
}
