using System;
using TMPro;
using UnityEngine;

public class TimeTest : MonoBehaviour {
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;


    private void Start() {
        text1 = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text2 = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text3 = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        text4 = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        text5 = this.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        text1.text = "" + GameManager.Instance.dreamValue;
        int sec = Mathf.FloorToInt(GameManager.Instance.seconds);
        text2.text = "" + GameManager.Instance.minutes.ToString("00") + ":" + sec.ToString("00");
        text3.text = "" + Time.timeSinceLevelLoad;
        text4.text = "" + Time.unscaledTime;
        text5.text = DateTime.Now.ToString("HH:mm");
    }
}
