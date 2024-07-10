using UnityEngine;
using UnityEngine.UI;

public class RenderTexture : MonoBehaviour
{
    public bool lowResolution;
    public Texture lowRes, highRes;
    public GameObject renderObj, pauseObj;

    void Start()
    {
        PlayerPrefs.SetInt("LowResolution", (lowResolution ? 1 : 0));
        lowResolution = (PlayerPrefs.GetInt("LowResolution") != 0);
        if (lowResolution)
        {
            GoLowRes();
        }
        else
        {
            GoHighRes();
        }
    }

    public void GoLowRes()
    {
        renderObj.GetComponent<RawImage>().texture = lowRes;
        pauseObj.GetComponent<RawImage>().texture = lowRes;
        lowResolution = true;
        Camera.main.targetTexture = (UnityEngine.RenderTexture)lowRes;
        Camera.main.targetTexture.Create();
    }

    public void GoHighRes()
    {
        renderObj.GetComponent<RawImage>().texture = highRes;
        pauseObj.GetComponent<RawImage>().texture = highRes;
        lowResolution = false;
        Camera.main.targetTexture = (UnityEngine.RenderTexture)highRes;
        Camera.main.targetTexture.Create();
    }
}
