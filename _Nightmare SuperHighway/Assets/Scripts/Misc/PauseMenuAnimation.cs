using UnityEngine;
using UnityEngine.UI;

public class PauseMenuAnimation : MonoBehaviour
{
	public float _distortionStrength;
	public float unscaledTime;
	public Material mat;
	public Texture thisTexture;
	public Animator anim;

	private void Start()
	{
		mat = GetComponent<RawImage>().material;
		thisTexture = GetComponent<RawImage>().texture;
    }

	private void Update()
	{
		
		if (GameManager.Instance.pauseMenu == true)
		{
			Paused();
            anim.SetBool("pausedMenu", true);
        }
		if (GameManager.Instance.pauseMenu == false)
		{
			UnPaused();
            anim.SetBool("pausedMenu", false);
        }
	}

	private void Paused()
	{
		
		anim.SetBool("pausedMenu", true);
        mat.SetTexture("_Texture2D", thisTexture);
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
		mat.SetFloat("_DistortionStrength", _distortionStrength);

    }

	public void PlayAnimation()
	{
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
        mat.SetTexture("_Texture2D", thisTexture);
        mat.SetFloat("_DistortionStrength", _distortionStrength);
    }

	private void UnPaused()
	{
		anim.SetBool("pausedMenu", false);
        mat.SetTexture("_Texture2D", thisTexture);
        mat.SetFloat("_UnscaledTime", Time.unscaledTime);
        mat.SetFloat("_DistortionStrength", _distortionStrength);

    }
}
