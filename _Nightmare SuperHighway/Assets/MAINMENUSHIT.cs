using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class MAINMENUSHIT : MonoBehaviour
{
    public GameObject settObj;
    public GameObject bgImg;
    public GameObject introObj;
    Resolution[] resolutions;
    public Dropdown resolitionDropdown;
    const string resName = "resolutionoption";
    public Toggle fullscreenToggle;
    private int screenInt;

    // Start is called before the first frame update
    void Awake()
    {
        screenInt = PlayerPrefs.GetInt("togglestate");
        if (screenInt != 0)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
        resolitionDropdown.onValueChanged.AddListener(new UnityAction<int>(index => {
            PlayerPrefs.SetInt(resName, resolitionDropdown.value);
            PlayerPrefs.Save();
        }));
    }

    private void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolitionDropdown.ClearOptions();
        List<string> options = new();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolitionDropdown.AddOptions(options);
        resolitionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolitionDropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen == false)
        {
            PlayerPrefs.SetInt("togglestate", 0);
        }
        else
        {
            PlayerPrefs.SetInt("togglestate", 1);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void CloseGame()
    {
        //Doesn't work inside Unity editor.
        Application.Quit();
    }

    public void StartLowRes()
    {
        PlayerPrefs.SetInt("LowResolution", 1);
        bgImg.SetActive(false);
        settObj.SetActive(false);
        introObj.SetActive(true);
    }
    public void StartHighRes()
    {
        PlayerPrefs.SetInt("LowResolution", 0);
        bgImg.SetActive(false);
        settObj.SetActive(false);
        introObj.SetActive(true);
    }

}
