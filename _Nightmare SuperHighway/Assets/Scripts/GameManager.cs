using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Variables
	public bool pauseMenu;
	public bool isLoading;
	public bool controllable;
	public float seconds = 00;
	public float minutes = 00;
	public float dreamValue = 0;
	public Image img;
	#endregion

	#region PlayerStats
	public bool DEBUG_MODE = false;
	public bool doesSaveFileExist = false;
	#endregion

	#region Hidden
	[HideInInspector] public LedgeClimbing climbing;
	[HideInInspector] public Movement player;
	#endregion

	/*Saved files can be found in Registry under: 
    *HKEY_CURRENT_USER\Software\Unity\UnityEditor\[company name]\[project name]*/
	public static GameManager Instance { get; private set; }

	private void Awake()
	{
		//Remove others if scene contains multiple game managers
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
		player = FindFirstObjectByType<Movement>();
		climbing = FindFirstObjectByType<LedgeClimbing>();
		dreamValue = PlayerPrefs.GetFloat("Dream", 0);
		seconds = PlayerPrefs.GetFloat("timeSeconds", 0f);
		minutes = PlayerPrefs.GetFloat("timeMinutes", 0f);

        DEBUG_MODE = (PlayerPrefs.GetInt("DEBUG_MODE") != 0);
		doesSaveFileExist = (PlayerPrefs.GetInt("SaveFileExist") != 0);
	}
	private void Update()
	{
		//Keeps track of playtime.
		seconds += Time.unscaledDeltaTime;
		if (seconds >= 60)
		{
			minutes++;
			seconds = 00;
		}
	}

	public IEnumerator GamePause()
	{
		controllable = false;
		Cursor.lockState = CursorLockMode.None;
		Time.timeScale = 0f;
		Cursor.visible = true;
		AudioListener.pause = true;
		yield return null;
	}

	public IEnumerator GameUnpause()
	{
		controllable = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;
		AudioListener.pause = false;
		yield return null;
	}

	public IEnumerator ForceSetControllable()
	{
		controllable = true;
		yield return null;
	}

	public IEnumerator UpdateDream(float amount)
	{
		
		dreamValue += amount;
		//Makes sure dream value cannot go over 100 or under 0
		if (dreamValue >= 100)
		{
			dreamValue = 100;
		}
		if (dreamValue <= 0)
		{
			dreamValue = 0;
		}
		PlayerPrefs.SetFloat("Dream", dreamValue);
		yield return null;
	}

	public IEnumerator DEBUG_MODE_ON()
	{
		//DEBUG_MODE = true;
		//PlayerPrefs.SetInt("DEBUG_MODE", (true ? 1 : 0));
		yield return null;
	}

	public IEnumerator StartFirstTime()
	{
		//doesSaveFileExist = true;
		//PlayerPrefs.SetInt("SaveFileExist", (true ? 1 : 0));
		yield return null;
	}

	public IEnumerator AutoSave()
	{
		//PlayerPrefs.SetFloat("timeSeconds", seconds);
		//PlayerPrefs.SetFloat("timeMinutes", minutes);
		yield return null;
	}

	private void OnApplicationQuit()
	{
		//PlayerPrefs.SetFloat("timeSeconds", seconds);
		//PlayerPrefs.SetFloat("timeMinutes", minutes);
		PlayerPrefs.SetInt("SpawnPoint", 0);
		PlayerPrefs.DeleteAll();
    }
}