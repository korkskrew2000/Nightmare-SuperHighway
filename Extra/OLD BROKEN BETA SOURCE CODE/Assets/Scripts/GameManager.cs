using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Variables
	public bool pauseMenu;
	public bool subMenu;
	public bool controllable;
	public float seconds = 00;
	public float minutes = 00;
	public float dreamValue = 0;
	#endregion

	#region PlayerStats
	public bool DEBUG_MODE = false;
	public bool doesSaveFileExist = false;
	public bool haveEffect_Slap = false;
	public float timesJumped = 0;
	public float timesTeleported = 0;
	public float foodEaten = 0;
	public float itemsGathered = 0;
	#endregion

	#region Hidden
	[HideInInspector] public LedgeClimbing climbing;
	[HideInInspector] public Movement player;
	[HideInInspector] public LevelChangeManager levelManager;
	[HideInInspector] public Image img;
	[HideInInspector] public DialogueUI dialogueUI;
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
		player = FindObjectOfType<Movement>();
		levelManager = FindObjectOfType<LevelChangeManager>();
		dialogueUI = FindObjectOfType<DialogueUI>();
		climbing = FindObjectOfType<LedgeClimbing>();
		StartCoroutine(FadeEffect(255));
		dreamValue = PlayerPrefs.GetFloat("Dream", 0);
		seconds = PlayerPrefs.GetFloat("timeSeconds", 0f);
		minutes = PlayerPrefs.GetFloat("timeMinutes", 0f);
		img.gameObject.SetActive(true);

		DEBUG_MODE = (PlayerPrefs.GetInt("DEBUG_MODE") != 0);
		doesSaveFileExist = (PlayerPrefs.GetInt("SaveFileExist") != 0);
		haveEffect_Slap = (PlayerPrefs.GetInt("HaveAttackEffect") != 0);


		//PlayerStats
		timesJumped = PlayerPrefs.GetFloat("PlayerStats_timesJumped", 0);
		timesTeleported = PlayerPrefs.GetFloat("PlayerStats_timesTeleported", 0);
		foodEaten = PlayerPrefs.GetFloat("PlayerStats_foodEaten", 0);
		itemsGathered = PlayerPrefs.GetFloat("PlayerStats_itemsGathered", 0);


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

	public void GetEffectAttack()
	{
		haveEffect_Slap = true;
		PlayerPrefs.SetInt("HaveAttackEffect", (true ? 1 : 0));
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

	public IEnumerator SpeakerResponses()
	{
		controllable = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		yield return null;
	}

	public IEnumerator SpeakerSetFree()
	{
		controllable = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		yield return null;
	}

	public IEnumerator GameUnpause()
	{
		controllable = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (dialogueUI.inResponses)
		{
			StartCoroutine(SpeakerResponses());
		}
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


	public IEnumerator FadeEffect(float alpha)
	{
		img = GameObject.Find("FadeImage").GetComponent<Image>();
		Color c = img.color;
		c.a = alpha;
		img.color = c;
		yield return null;
	}

	public IEnumerator FadeIn(float fadeSpeed, float delay)
	{
		controllable = false;
		yield return new WaitForSeconds(delay);
		img.CrossFadeAlpha(1.0f, fadeSpeed, true);
	}

	public IEnumerator FadeOut(float fadeSpeed, float delay)
	{
		yield return new WaitForSeconds(delay);
		img.CrossFadeAlpha(0.0f, fadeSpeed, true);
		controllable = true;
	}

	public IEnumerator DEBUG_MODE_ON()
	{
		DEBUG_MODE = true;
		PlayerPrefs.SetInt("DEBUG_MODE", (true ? 1 : 0));
		yield return null;
	}

	public IEnumerator StartFirstTime()
	{
		doesSaveFileExist = true;
		PlayerPrefs.SetInt("SaveFileExist", (true ? 1 : 0));
		yield return null;
	}

	public IEnumerator AutoSave()
	{
		PlayerPrefs.SetFloat("timeSeconds", seconds);
		PlayerPrefs.SetFloat("timeMinutes", minutes);
		PlayerPrefs.SetFloat("Dream", dreamValue);
		PlayerPrefs.SetFloat("PlayerStats_timesJumped", timesJumped);
		PlayerPrefs.SetFloat("PlayerStats_timesTeleported", timesTeleported);
		PlayerPrefs.SetFloat("PlayerStats_foodEaten", foodEaten);
		PlayerPrefs.SetFloat("PlayerStats_itemsGathered", itemsGathered);
		yield return null;
	}

	private void OnApplicationQuit()
	{
		//Saves all of these values when the game is closed.
		PlayerPrefs.SetFloat("timeSeconds", seconds);
		PlayerPrefs.SetFloat("timeMinutes", minutes);
		PlayerPrefs.SetFloat("Dream", dreamValue);
		PlayerPrefs.SetFloat("PlayerStats_timesJumped", timesJumped);
		PlayerPrefs.SetFloat("PlayerStats_timesTeleported", timesTeleported);
	}
}