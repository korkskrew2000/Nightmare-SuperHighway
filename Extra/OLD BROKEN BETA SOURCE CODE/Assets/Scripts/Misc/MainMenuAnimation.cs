using UnityEngine;
using UnityEngine.UI;


public class MainMenuAnimation : MonoBehaviour {
    Material mat;
    RawImage img;

    void Start() {
        img = GetComponent<RawImage>();
        mat = GetComponent<RawImage>().material;
    }
    private void Update() {
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
    }
}
