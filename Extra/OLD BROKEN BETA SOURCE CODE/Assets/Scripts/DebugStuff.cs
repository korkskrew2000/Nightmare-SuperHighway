using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugStuff : MonoBehaviour {
    #region Debug Variables
    public bool DebugModeActive;
    bool debugMenuOpen;
    public GameObject debugTab;
    public MenuScript menuScript;
    public TextMeshProUGUI debugText1;
    public TextMeshProUGUI debugText5;
    public int inputValue;
    public GameObject errorText;
    public int currentLevel;
    public int currentAmountOfSpawnPoints;
    Scene scene;
    #endregion

    private void Start() {
        scene = SceneManager.GetActiveScene();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentAmountOfSpawnPoints = GameObject.Find("_spawnPoints").transform.childCount;
    }

    void Update() {
        if (Input.GetButtonDown("Enable Debug Button")) {
            debugMenuOpen = !debugMenuOpen;
        }
        if (debugMenuOpen) {
            debugTab.gameObject.SetActive(true);
        } else {
            debugTab.gameObject.SetActive(false);
        }
        if (debugTab == true) {
            debugText1.text = "Dream : " + GameManager.Instance.dreamValue;

            int sec = Mathf.FloorToInt(GameManager.Instance.seconds);
            debugText5.text =
                "Level : " + scene.name + "\n" +
                "PlayerStats" + "\n" +
                "Jumps : " + GameManager.Instance.timesJumped + "\n" +
                "Teleports : " + GameManager.Instance.timesTeleported + "\n" +
                "Time : " + GameManager.Instance.minutes.ToString("00") + ":" + sec.ToString("00");
        }

        if (DebugModeActive == false) {
            debugTab.gameObject.SetActive(false);
        }
    }

    public void InputChanged(InputField input) {
        inputValue = int.Parse(input.text);
    }

    public void WarptoLevel() {
        if (Application.CanStreamedLevelBeLoaded(inputValue)) {
            SceneManager.LoadScene(inputValue);
            Debug.Log("Level warp used");
        } else {
            errorText.gameObject.SetActive(true);
            Debug.Log("False level code");
        }
    }

    public void OpenDebug() {
        debugTab.gameObject.SetActive(true);
    }

    public void RestartLevel() {
        StartCoroutine(GameManager.Instance.AutoSave());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Debug Reset Level");
    }

    public void AddAttackEffect() {
        GameManager.Instance.GetEffectAttack();
    }

    public void AddDream1() {
        StartCoroutine(GameManager.Instance.UpdateDream(1f));
        Debug.Log("Debug Dream +1");
    }
    public void AddDream5() {
        StartCoroutine(GameManager.Instance.UpdateDream(5f));
        Debug.Log("Debug Dream +5");
    }
    public void DecreaseDream1() {
        StartCoroutine(GameManager.Instance.UpdateDream(-1f));
        Debug.Log("Debug Dream -1");
    }
    public void DecreaseDream5() {
        StartCoroutine(GameManager.Instance.UpdateDream(-5f));
        Debug.Log("Debug Dream -5");
    }
}
