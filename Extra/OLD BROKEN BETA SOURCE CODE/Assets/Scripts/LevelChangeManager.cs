using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeManager : MonoBehaviour
{
	#region Public Variables
	public Transform spawnPoints;
	[Space(10)]
	public int levelToLoad;
	public int givenSpawnNumber;
	public Transform whereToSpawn;
	#endregion

	#region Private Variables
	private GameObject player;
	private Camera playerCam;
	private CrossHair crosshair;
	private bool fadingIn = false;
	private RaycastHit _hit;
	#endregion

	public void Start()
	{
		givenSpawnNumber = PlayerPrefs.GetInt("SpawnPoint");
		player = GameManager.Instance.player.gameObject;
		playerCam = Camera.main;
		FindSpawnPoints();

		whereToSpawn = spawnPoints.GetChild(givenSpawnNumber);
		player.transform.SetPositionAndRotation(whereToSpawn.position, whereToSpawn.rotation);

		crosshair = Camera.main.GetComponent<CrossHair>();
		StartCoroutine(GameManager.Instance.AutoSave());
		StartCoroutine(GameManager.Instance.FadeOut(5f, 1f));
	}

	public void Update()
	{
		if (crosshair.crosshairState == CrossHair.State.Teleporter)
		{
			_hit = crosshair.normalHit;
			Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green);
			if (Input.GetButtonDown("Interact"))
			{
				ChangeLevel(_hit.collider.transform.root.gameObject.GetComponent<LevelChangeScript>().fadeSpeed);
			}
		}
		//Once it's fully faded to black the next level is loaded.
		if (GameManager.Instance.img.canvasRenderer.GetAlpha() == 1.0f && fadingIn == true)
		{
			SceneManager.LoadScene(levelToLoad);
		}
	}

	public void ChangeLevel(float fadespeed)
	{
		fadingIn = true;
		GameObject tpObj = _hit.collider.transform.root.gameObject;
		LevelChangeScript lvlScript = tpObj.GetComponent<LevelChangeScript>();
		givenSpawnNumber = lvlScript.spawnPointNumber;
		PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
		levelToLoad = lvlScript.levelValue;

		StartCoroutine(GameManager.Instance.UpdateDream(lvlScript.addToDreamValue));
		GameManager.Instance.timesTeleported++;

		StartCoroutine(GameManager.Instance.FadeIn(fadespeed, 0f));
	}

	public void ForceChangeLevel(GameObject ftp, float fadespeed)
	{
		fadingIn = true;
		LevelChangeScript lvlScript = ftp.GetComponent<LevelChangeScript>();
		givenSpawnNumber = lvlScript.spawnPointNumber;
		PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
		levelToLoad = lvlScript.levelValue;

		StartCoroutine(GameManager.Instance.UpdateDream(lvlScript.addToDreamValue));
		GameManager.Instance.timesTeleported++;

		StartCoroutine(GameManager.Instance.FadeIn(fadespeed, 0f));
	}

	public void FindSpawnPoints()
	{
		//Object named "spawnPoints" has to be present in a scene.
		if (GameObject.Find("_spawnPoints") == null)
		{
			Debug.LogError("_spawnPoints object is missing.");
			Debug.Break();
		}
		else
		{
			spawnPoints = GameObject.Find("_spawnPoints").transform;
		}
		if (givenSpawnNumber > spawnPoints.childCount - 1)
		{
			Debug.LogError("givenSpawnNumber was: " + givenSpawnNumber + " but it recieved " + (spawnPoints.childCount - 1) + " instead.");
			givenSpawnNumber = 0;
		}
	}
}