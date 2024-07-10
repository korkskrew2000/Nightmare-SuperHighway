using UnityEngine;
using UnityEngine.UI;

public class PauseMenuAnimation : MonoBehaviour
{
	public float distortionStrength;
	public float alpha;
	public float float2;
	public float WHITEFLASH;
	private Material mat;
	[SerializeField] private Animator anim;
	private bool paused;

	private void Start()
	{
		mat = GetComponent<RawImage>().material;
	}

	private void Update()
	{
		paused = GameManager.Instance.pauseMenu;
		if (paused == true)
		{
			Paused();
		}
		if (paused == false)
		{
			UnPaused();
		}
	}

	private void Paused()
	{
		
		anim.SetBool("openMenu", true);
		mat.SetFloat("_UnscaledTime", Time.unscaledTime);
		mat.SetFloat("_DistortionStrength", distortionStrength);
		mat.SetFloat("_Alpha", alpha);
		mat.SetFloat("_Float_2", float2);
		mat.SetFloat("_WHITEFLASHEFFECT", WHITEFLASH);

	}

	private void UnPaused()
	{
		anim.SetBool("openMenu", false);
		mat.SetFloat("_DistortionStrength", distortionStrength);
		mat.SetFloat("_Alpha", alpha);
		mat.SetFloat("_Float_2", float2);
		mat.SetFloat("_WHITEFLASHEFFECT", WHITEFLASH);
		
	}
}
