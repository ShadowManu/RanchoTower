using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour{ 

    public Text amountText;
    public Resource gold;

    // Start is called before the first frame update
    void Start()
    {
        amountText = GameObject.Find("ResourceText").GetComponent<Text>();
        gold = GameObject.Find("Gold").GetComponent<Resource>();
        Debug.Log(gold.amount);
        SetAmountText();
    }

    // Update is called once per frame
    void Update()
    {
        SetAmountText();
    }

    void SetAmountText(){
        amountText.text = "Gold: " + gold.amount.ToString ();
    }
}
