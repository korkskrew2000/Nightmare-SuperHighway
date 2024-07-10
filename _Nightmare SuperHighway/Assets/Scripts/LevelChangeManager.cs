using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeManager : MonoBehaviour
{
    public static LevelChangeManager instance;
    #region Public Variables
    public Transform spawnPoints;
	[Space(10)]
	public int levelToLoad;
	public int givenSpawnNumber;
	public Transform whereToSpawn;
    public CrossHair crosshair;
	public Animator anim;
	public PauseMenuAnimation pauseAnim;
    public bool isLoading;
    #endregion

    #region Private Variables
    private GameObject player;
	private Camera playerCam;
	private RaycastHit _hit;
	#endregion

	public void Start()
	{
        if (instance == null)
        {
            instance = this;
        }
        givenSpawnNumber = PlayerPrefs.GetInt("SpawnPoint");
		player = GameManager.Instance.player.gameObject;
		playerCam = Camera.main;
		FindSpawnPoints();

		whereToSpawn = spawnPoints.GetChild(givenSpawnNumber);
		player.transform.SetPositionAndRotation(whereToSpawn.position, whereToSpawn.rotation);

		StartCoroutine(GameManager.Instance.AutoSave());
        
        StartCoroutine(FadeIn());

	}

	public void Update()
	{
		if (crosshair.crosshairState == CrossHair.State.Teleporter)
		{
			_hit = crosshair.normalHit;
			Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green);
			if (Input.GetButtonDown("Interact"))
			{
                StartCoroutine(LoadLevel(_hit.collider.transform.root.gameObject.GetComponent<LevelChangeScript>().levelValue));
            }
		}
	}


    public IEnumerator FadeIn(){
        pauseAnim.PlayAnimation();
        GameManager.Instance.isLoading = false;
        GameManager.Instance.controllable = true;
        yield return null;
    }
    public IEnumerator LoadLevel(int levelIndex){
        GameManager.Instance.controllable = false;
        GameManager.Instance.isLoading = true;
        anim.SetBool("Transition", true);
        GameObject tpObj = _hit.collider.transform.root.gameObject;
        LevelChangeScript lvlScript = tpObj.GetComponent<LevelChangeScript>();
        givenSpawnNumber = lvlScript.spawnPointNumber;
        PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
        yield return new WaitForSeconds(3);
        levelToLoad = levelIndex;
        StartCoroutine(GameManager.Instance.UpdateDream(lvlScript.addToDreamValue));
        SceneManager.LoadScene(levelToLoad);
    }

    public IEnumerator ForceLoadLevel(int levelIndex, int spawnNumber)
    {
        GameManager.Instance.controllable = false;
        GameManager.Instance.isLoading = true;
        anim.SetBool("Transition", true);
		givenSpawnNumber = spawnNumber;
        PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
        yield return new WaitForSeconds(3);
        levelToLoad = levelIndex;
        SceneManager.LoadScene(levelToLoad);
    }

    public void ChangeLevel(float fadespeed)
	{
		GameObject tpObj = _hit.collider.transform.root.gameObject;
		LevelChangeScript lvlScript = tpObj.GetComponent<LevelChangeScript>();
		givenSpawnNumber = lvlScript.spawnPointNumber;
		PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
		levelToLoad = lvlScript.levelValue;

		StartCoroutine(GameManager.Instance.UpdateDream(lvlScript.addToDreamValue));
	}

	public void ForceChangeLevel(GameObject ftp, float fadespeed)
	{
		LevelChangeScript lvlScript = ftp.GetComponent<LevelChangeScript>();
		givenSpawnNumber = lvlScript.spawnPointNumber;
		PlayerPrefs.SetInt("SpawnPoint", givenSpawnNumber);
		levelToLoad = lvlScript.levelValue;

		StartCoroutine(GameManager.Instance.UpdateDream(lvlScript.addToDreamValue));
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