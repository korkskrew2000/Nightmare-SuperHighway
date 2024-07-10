using TMPro;
using UnityEngine;
using System.Collections;


public class MenuScript : MonoBehaviour
{

	public TMP_Text targetChannel, currentChannel, dateTime, currentTime;

	public int currentChannelValue = 100;

	public int targetValue;

	public int menuValue;

	public GameObject menuTab, settingsTab, debugTab, overlayTab;

	public GameObject videoSettings, inputSettings, audioSettings, buttons;
	private bool calculateTime = false;

	public float timer;
	private bool isCalculating;
	private bool isSearching = true;
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
		if (currentChannelValue != targetValue && isCalculating)
		{
			timer += Time.unscaledDeltaTime;
			targetChannel.text = targetValue.ToString();
			if (timer >= 0.05f)
			{
				timer = 0f;
				currentChannelValue += 1;
				currentChannel.text = currentChannelValue.ToString();
				if (currentChannelValue > 200)
				{
					currentChannelValue = 100;
				}
			}
			if (currentChannelValue == targetValue)
			{
				isCalculating = false;
				isSearching = true;
			}
		}
		if (currentChannelValue == targetValue && isSearching)
		{
			targetChannel.text = targetValue.ToString();
			currentChannel.text = currentChannelValue.ToString();
			WhatToOpen(menuValue);
			isSearching = false;
		}


		if (Input.GetButtonDown("Pause") && !menuDelay && notInOptions)
		{
			GameManager.Instance.pauseMenu = !GameManager.Instance.pauseMenu;
			if (GameManager.Instance.pauseMenu)
			{
				StartCoroutine(GameManager.Instance.GamePause());
				menuTab.SetActive(true);
				buttons.SetActive(true);
				overlayTab.SetActive(true);
				inputSettings.SetActive(false);
				audioSettings.SetActive(false);
				videoSettings.SetActive(false);
				AudioListener.volume = 0.3f;

				calculateTime = true;
				menuDelay = true;
				StartCoroutine(MenuOpenDelay(3));

			}
			if (!GameManager.Instance.pauseMenu)
			{
				UnPause();
				menuDelay = true;
				StartCoroutine(MenuOpenDelay(1));
				overlayTab.SetActive(false);
				calculateTime = false;
			}
		}

		if (calculateTime)
		{
			dateTime.text = System.DateTime.Now.ToString("ddd/dd/MMM");
			currentTime.text = System.DateTime.Now.ToString("HH/mm/ss");
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
				debugTab.SetActive(false);

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
		debugTab.SetActive(false);
		AudioListener.volume = 1.0f;
	}

	public void OpenOptions()
	{
		targetValue = 113;
		menuValue = 1;
		isCalculating = true;
		ResetIfBigger();
	}

	public void OpenAudio()
	{
		targetValue = 125;
		menuValue = 2;
		isCalculating = true;
		ResetIfBigger();
	}

	public void OpenVideo()
	{
		targetValue = 136;
		menuValue = 3;
		isCalculating = true;
		ResetIfBigger();
	}

	public void OpenInput()
	{
		targetValue = 152;
		menuValue = 4;
		isCalculating = true;
		ResetIfBigger();
	}

	public void CloseOptions()
	{
		targetValue = 113;
		menuValue = 1;
		isCalculating = true;
		ResetIfBigger();
	}

	public void CloseGame()
	{
		//Doesn't work inside Unity editor.
		Application.Quit();
	}

	public void ResetIfBigger()
	{
		if(currentChannelValue > targetValue)
		{
			currentChannelValue = 100;
		}
	}

	IEnumerator MenuOpenDelay(float Secs)
	{
		yield return new WaitForSecondsRealtime(Secs);
		menuDelay = false;
	}

}
