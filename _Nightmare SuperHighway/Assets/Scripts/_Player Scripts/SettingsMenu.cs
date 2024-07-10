using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolitionDropdown;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;
    private int screenInt;
    public Toggle fullscreenToggle;
    public Toggle fourxthreeToggle;
    const string resName = "resolutionoption";

    private void Awake() {
        screenInt = PlayerPrefs.GetInt("togglestate");
        if (screenInt == 1) {
            fullscreenToggle.isOn = true;
        } else {
            fullscreenToggle.isOn = false;
        }
        resolitionDropdown.onValueChanged.AddListener(new UnityAction<int>(index => {
            PlayerPrefs.SetInt(resName, resolitionDropdown.value);
            PlayerPrefs.Save();
        }));
    }
    private void Start() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Mastervolume", 0);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Mastervolume"));
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Musicvolume", 0);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Musicvolume"));
        effectVolumeSlider.value = PlayerPrefs.GetFloat("Effectvolume", 0);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Effectvolume"));
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolitionDropdown.ClearOptions();
        List<string> options = new();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }
        resolitionDropdown.AddOptions(options);
        resolitionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolitionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume) {
        PlayerPrefs.SetFloat("Mastervolume", volume);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Mastervolume"));
    }
    public void SetMusicVolume(float volume) {
        PlayerPrefs.SetFloat("Musicvolume", volume);
        audioMixer.SetFloat("music", PlayerPrefs.GetFloat("Musicvolume"));
    }
    public void SetEffectVolume(float volume) {
        PlayerPrefs.SetFloat("Effectvolume", volume);
        audioMixer.SetFloat("effect", PlayerPrefs.GetFloat("Effectvolume"));
    }
    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen == false) {
            PlayerPrefs.SetInt("togglestate", 0);
        } else {
            PlayerPrefs.SetInt("togglestate", 1);
        }
    }
}
