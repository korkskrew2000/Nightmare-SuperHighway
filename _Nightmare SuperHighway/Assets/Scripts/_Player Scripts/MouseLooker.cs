using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseLooker : MonoBehaviour
{
#pragma warning disable 649
	public float mouseX;
	public float mouseY;
	private float xClamp = 85f;
	public float xRotation = 0f;
	public CameraAnimator camAnim;
	public Slider HorizontalSlider;
	public Slider VerticalSlider;
	public float HorizontalSensitivity;
	public float VerticalSensitivity;
	public TextMeshProUGUI hozNumber;
	public TextMeshProUGUI verNumber;
	public Transform playerBody;
	public MenuScript menuscript;

	private void Start()
	{
		if (HorizontalSlider)
		{
			HorizontalSensitivity = PlayerPrefs.GetFloat("HorizontalMouseSpeed", 50f);
			HorizontalSlider.value = HorizontalSensitivity;
			hozNumber.text = ": " + HorizontalSensitivity.ToString();
		}
		if (VerticalSlider)
		{
			VerticalSensitivity = PlayerPrefs.GetFloat("VerticalMouseSpeed", 50f);
			VerticalSlider.value = VerticalSensitivity;
			verNumber.text = ": " + VerticalSensitivity.ToString();
		}
	}

	private void Update()
	{
		if (GameManager.Instance.controllable)
		{
			if (camAnim.cameraIsNotPlayingAnimation)
			{
			mouseX = Input.GetAxis("Mouse X") * HorizontalSensitivity * Time.deltaTime;
			mouseY = Input.GetAxis("Mouse Y") * VerticalSensitivity * Time.deltaTime;
			}
			else
			{
				mouseX = 0;
				mouseY = 0;
				if(xRotation != 0)
				{
				xRotation = Mathf.MoveTowards(xRotation, 0f, 450f * Time.deltaTime);
				}
			}
			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -xClamp,
			xClamp); //Makes sure you can't over rotate and look behind (90 degrees up and down)
					 //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			transform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
			playerBody.Rotate(Vector3.up * mouseX);

		}

	}

	public void SetHorizontal(float speed)
	{
		HorizontalSensitivity = speed;
		hozNumber.text = ":" + HorizontalSensitivity;
		PlayerPrefs.SetFloat("HorizontalMouseSpeed", speed);
		//Value is saved upon changing it.
	}
	public void SetVertical(float speed)
	{
		VerticalSensitivity = speed;
		verNumber.text = ":" + VerticalSensitivity;
		PlayerPrefs.SetFloat("VerticalMouseSpeed", speed);
	}
}
