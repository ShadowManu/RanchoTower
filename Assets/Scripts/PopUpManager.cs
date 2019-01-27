using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum PopUpType {damage, production, health}
public class PopUpManager : MonoBehaviour
{

    public Transform t; 

    public GameObject damagePopUp;
    public GameObject productionPopUp;

    private GameObject popUpPrefab;
    private PopUpType popUpType;
    public void CreateNumericlPopUp(PopUpType pt, int value, Transform spawnPoint){
        popUpType = pt;
        if (popUpType == PopUpType.damage){
           popUpPrefab = Instantiate(damagePopUp);
            popUpPrefab.transform.GetChild(1).GetComponent<Text>().text = "+";

       } else if (popUpType == PopUpType.production){
            popUpPrefab = Instantiate(productionPopUp);
            popUpPrefab.transform.GetChild(1).GetComponent<Text>().text = "+";
       }
        popUpPrefab.transform.GetChild(1).GetComponent<Text>().text += value.ToString();
        popUpPrefab.transform.position= spawnPoint.position;
        popUpPrefab.transform.LookAt(Camera.main.transform);
        popUpPrefab.GetComponent<CanvasGroup>().DOFade(0,0);
        StartCoroutine(BasicAnimation(popUpPrefab));
    }

//    public void setText(int value){
//        Text popUpText;
//        if (PopUpType == damage){
//            popUpText = "-" + value.ToString();
//        } else if (PopUpType = production){
//            popUpText = "+" + value.ToString();
//        }
//    }

   void Start(){
       CreateNumericlPopUp(PopUpType.production, 2, t);
   }

   IEnumerator BasicAnimation(GameObject popUp)
   {
       popUp.GetComponent<CanvasGroup>().DOFade(1, .25f);
       yield return popUp.transform.DOLocalMoveY(1.25f, 1f).SetRelative(true).SetEase(Ease.OutQuad).WaitForCompletion();
       yield return new WaitForSeconds(.4f);
       yield return popUp.GetComponent<CanvasGroup>().DOFade(0, .25f).WaitForCompletion();

       Destroy(popUp);
       
   }
}
