using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour {
    public GameObject menuTab, settingsTab;
    public GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;
    public bool saveExist;

    void Start() {
        saveExist = GameManager.Instance.doesSaveFileExist;
        PlayerPrefs.SetInt("SaveFileExist", (saveExist ? 1 : 0));
        saveExist = (PlayerPrefs.GetInt("SaveFileExist") != 0);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);

        if (saveExist) {
            //random title screen
        } else {
            //normal title screen
        }
    }

    public void OpenOptions() {
        settingsTab.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions() {
        settingsTab.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void CloseGame() {
        //Doesn't work inside Unity editor.
        Application.Quit();
    }

}
