using UnityEngine;

public class NewsTicker : MonoBehaviour
{
    public NewsTickerText textPrefab;
    public float itemDuration = 3.0f;
    public string textGap = " | ";
    [Header("0 should stay empty.")]
    public string[] fillerText;

    float width;
    float pixelsPerSecond;
    NewsTickerText currentText;
    bool started = false;

    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSecond = width / itemDuration;
        AddTickerItem(fillerText[0]);
        started = true;
    }

    void Update()
    {
        if(currentText.GetXPosition <= -currentText.GetWidth && started)
		{
            AddTickerItem(fillerText[Random.Range(1, fillerText.Length)] + textGap);
		}
    }

    void AddTickerItem(string message)
	{
        currentText = Instantiate(textPrefab, transform);
        currentText.Initialize(width, pixelsPerSecond, message);
	}
}
