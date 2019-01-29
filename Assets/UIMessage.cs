using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIMessage : MonoBehaviour
{
    public CanvasGroup c;
    public Text score; 
    // Start is called before the first frame update

    void Start()
    {
        c = GameObject.Find("ScoreScreen").GetComponent<CanvasGroup>();
        score = c.transform.GetChild(1).GetComponent<Text>();
    }
    public void Show()
    {
        score.text = EnemiesState.instance.currentWave.ToString();
        c.DOFade(1, .5f);
        Invoke("Exit", 4);
        
    }

    void Exit()
    {
        SceneManager.LoadScene(0);
    }

    
}
