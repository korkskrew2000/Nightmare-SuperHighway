using UnityEngine;
using UnityEngine.UI;

public class NewsTickerText : MonoBehaviour
{
	float tickerWidth;
	float pixelsPerSecond;
	RectTransform rt;

	public Text textMessage => GetComponent<Text>(); 
	public float GetXPosition => rt.anchoredPosition.x;
	public float GetWidth => rt.rect.width;

	public void Initialize(float tickerWidth, float pixelsPerSecond, string message)
	{
		this.tickerWidth = tickerWidth;
		this.pixelsPerSecond = pixelsPerSecond;
		rt = GetComponent<RectTransform>();
		GetComponent<Text>().text = message;
	}

	void Update()
	{
		rt.position += Vector3.left * pixelsPerSecond * Time.deltaTime;
		if(GetXPosition <= 0 - tickerWidth - GetWidth)
		{
			Destroy(gameObject);
		}
	}
}
