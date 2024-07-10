using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FOVChanger : MonoBehaviour
{
    public float setFieldofView;
    public TextMeshProUGUI fovText;
    public Slider fovSlider;

    void Start()
    {
        setFieldofView = PlayerPrefs.GetFloat("FieldOfView", 90f);
        fovSlider.value = setFieldofView;
        fovText.text = ": " + setFieldofView.ToString();
        Camera.main.fieldOfView = setFieldofView;
    }

    public void ChangeFov (float fov) {
        setFieldofView = fov;
        fovText.text = ": " + fov;
        Camera.main.fieldOfView = fov;
        PlayerPrefs.SetFloat("FieldOfView", fov);
    }
}
