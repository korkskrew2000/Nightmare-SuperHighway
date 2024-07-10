using UnityEngine;
using UnityEngine.UI;

public class RenderTexture : MonoBehaviour
{
    public bool goWideScreen = true;
    public Texture wideScreen, fourxthree;
    public GameObject renderObj, pauseObj;

    void Start()
    {
        if (goWideScreen)
        {
            GoWidescreen();
        }
        else
        {
            GoStandardResolution();
        }
    }

    public void GoWidescreen()
    {
        renderObj.GetComponent<RawImage>().texture = wideScreen;
        pauseObj.GetComponent<RawImage>().texture = wideScreen;
        goWideScreen = true;
        Camera.main.targetTexture = (UnityEngine.RenderTexture)wideScreen;
        Camera.main.targetTexture.Create();
    }

    public void GoStandardResolution()
    {
        renderObj.GetComponent<RawImage>().texture = fourxthree;
        pauseObj.GetComponent<RawImage>().texture = fourxthree;
        goWideScreen = false;
        Camera.main.targetTexture = (UnityEngine.RenderTexture)fourxthree;
        Camera.main.targetTexture.Create();
    }
}
