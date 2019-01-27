using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerUI : MonoBehaviour
{
	
	private Image fill;
	private CanvasGroup cg;
	private float time;
	public bool start = false;
	
    // Start is called before the first frame update
    void Start()
    {
		cg = GetComponent<CanvasGroup>();
		fill = transform.GetChild(0).GetComponent<Image>();
    }

	public void StartTimer(float t)
	{
		time = t;
		StartCoroutine(Timer());	
	}
	
	IEnumerator Timer()
	{
		//transform.DOMoveY(.5f, .25f).SetEase(Ease.OutQuad);
		cg.DOFade(1f, .25f);
		fill.fillAmount = 1;
		yield return fill.DOFillAmount(0f, time).WaitForCompletion();
		//transform.DOMoveY(-0.5f, .25f).SetEase(Ease.InQuad);
		cg.DOFade(0, .25f);
	}
	
	void Update()
	{
		if (start)
		{
			StartTimer(15);
			start = false;
		}
			
	}
}
