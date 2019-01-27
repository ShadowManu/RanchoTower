using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class InGameMenu : MonoBehaviour
{
	[SerializeField]
	GameObject PauseWindow;

	[SerializeField]
	GameObject SettingsWindow;

	[SerializeField]
	GameObject HelpWindow;

	[SerializeField]
	GameObject MenuUI;

	AudioManager audioManager;

	enum MenuStates { Playing, Pause, Settings, Help}
	MenuStates currentState;

	void Update()
	{

		if(Input.GetKeyDown("escape") && currentState== MenuStates.Pause)
		{
			currentState = MenuStates.Playing;
		}
		else if (Input.GetKeyDown("escape") && currentState == MenuStates.Playing)
		{
			currentState = MenuStates.Pause;
		}
		
		switch (currentState)
		{
			case MenuStates.Playing:
				currentState = MenuStates.Playing;
				PauseWindow.SetActive(false);
				SettingsWindow.SetActive(false);
				HelpWindow.SetActive(false);
				//MenuUI.SetActive(false);
				Time.timeScale = 1;

				break;
			case MenuStates.Pause:
				currentState = MenuStates.Pause;
				PauseWindow.SetActive(true);
				SettingsWindow.SetActive(false);
				HelpWindow.SetActive(false);
				//MenuUI.SetActive(false);
				Time.timeScale = 0;

				break;
			case MenuStates.Settings:
				currentState = MenuStates.Settings;
				PauseWindow.SetActive(false);
				SettingsWindow.SetActive(true);
				HelpWindow.SetActive(false);
				//MenuUI.SetActive(false);
				Time.timeScale = 0;

				break;
			case MenuStates.Help:

				currentState = MenuStates.Help;
				PauseWindow.SetActive(false);
				SettingsWindow.SetActive(false);
				HelpWindow.SetActive(true);
				//MenuUI.SetActive(false);
				Time.timeScale = 0;
				break;
		}

		//Activates or desactivates the construction menu
		if (Input.GetKeyDown(KeyCode.C) && currentState == MenuStates.Playing)
		{
			if (MenuUI.activeInHierarchy == false)
			{
				MenuUI.GetComponent<CanvasGroup>().DOFade(0, 0).WaitForCompletion();
				MenuUI.SetActive(true);
				MenuUI.GetComponent<CanvasGroup>().DOFade(1, .25f).WaitForCompletion();
			}
			else
			{
				StartCoroutine(HideUI());
			}
		}
	}



	public void Restart()
	{
		SceneManager.LoadScene(1);
	}

	public void Resume()
	{
		currentState = MenuStates.Playing;
	}

	public void DisplaySettings()
	{
		currentState = MenuStates.Settings;
	}
  
	public void DisplayHelp()
	{
		currentState = MenuStates.Help;
	}

	public void Exit()
	{
		UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}

	public void BackButton()
	{
		currentState = MenuStates.Pause;
	}

	private IEnumerator HideUI()
	{
		yield return MenuUI.GetComponent<CanvasGroup>().DOFade(0, .25f).WaitForCompletion();
		MenuUI.SetActive(false);
	}

	public void SetSFXVolume(float sfxLv)
	{
		audioManager.SetSFXVolume(sfxLv);
	}

	public void SetBGMVolume(float bgmLv)
	{
		audioManager.SetMusicVolume(bgmLv);
	}

	public void SetVoiceVolume(float voiceLv)
	{
		audioManager.SetVoiceVolume(voiceLv);
	}

	public void SetMasterVolume(float masterLv)
	{
		audioManager.SetMasterVolume(masterLv);
	}


}
