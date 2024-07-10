using TMPro;
using UnityEngine;
using System.Collections;


public class MenuScript : MonoBehaviour
{

	public TMP_Text currentTime;

	public GameObject menuTab, settingsTab;

	public GameObject videoSettings, inputSettings, audioSettings, buttons;

	public float timer;
	private bool menuDelay = false;
	private bool notInOptions = true;


	private void Awake()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause") && !menuDelay && notInOptions && !GameManager.Instance.isLoading)
		{
			GameManager.Instance.pauseMenu = !GameManager.Instance.pauseMenu;
			if (GameManager.Instance.pauseMenu)
			{
				StartCoroutine(GameManager.Instance.GamePause());
				menuTab.SetActive(true);
				buttons.SetActive(true);
				inputSettings.SetActive(false);
				audioSettings.SetActive(false);
				videoSettings.SetActive(false);
				AudioListener.volume = 0.3f;

				menuDelay = true;
				StartCoroutine(MenuOpenDelay(3));

			}
			if (!GameManager.Instance.pauseMenu)
			{
				UnPause();
				menuDelay = true;
				StartCoroutine(MenuOpenDelay(1));
			}
		}
	}

	public void WhatToOpen(int menu)
	{
		switch (menu)
		{
			case 0:
				//none
				settingsTab.SetActive(false);
				audioSettings.SetActive(false);
				inputSettings.SetActive(false);
				videoSettings.SetActive(false);
				notInOptions = true;
				break;

			case 1:
				//options
				buttons.SetActive(true);
				settingsTab.SetActive(true);

				audioSettings.SetActive(false);
				inputSettings.SetActive(false);
				videoSettings.SetActive(false);
				notInOptions = false;
				break;

			case 2:
				//audio
				audioSettings.SetActive(true);
				inputSettings.SetActive(false);
				videoSettings.SetActive(false);
				buttons.SetActive(false);
				notInOptions = false;
				break;

			case 3:
				//video
				videoSettings.SetActive(true);
				audioSettings.SetActive(false);
				inputSettings.SetActive(false);
				buttons.SetActive(false);
				notInOptions = false;
				break;

			case 4:
				//input
				inputSettings.SetActive(true);
				audioSettings.SetActive(false);
				videoSettings.SetActive(false);
				buttons.SetActive(false);
				notInOptions = false;
				break;
		}
	}
	public void UnPause()
	{
		GameManager.Instance.pauseMenu = false;
		StartCoroutine(GameManager.Instance.GameUnpause());
		menuTab.SetActive(false);
		settingsTab.SetActive(false);
        buttons.SetActive(false);
        inputSettings.SetActive(false);
        audioSettings.SetActive(false);
        videoSettings.SetActive(false);
        AudioListener.volume = 1.0f;
	}
	public void CloseGame()
	{
		//Doesn't work inside Unity editor.
		Application.Quit();
	}

	IEnumerator MenuOpenDelay(float Secs)
	{
		yield return new WaitForSecondsRealtime(Secs);
		menuDelay = false;
	}
}
