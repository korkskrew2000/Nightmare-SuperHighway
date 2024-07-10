using UnityEngine;

public class MovingBlock : MonoBehaviour {
    public float speed;
    public GameObject startPoint;
    public GameObject endPoint;
    public bool reverseMove = false;
    float startTime;
    float Length;

    //Some old moving block script I made. These shouldn't be used as platforms.

    void Start() {
        startTime = Time.time;
        Length = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
    }
    void Update() {
        float distance = (Time.time - startTime) * speed;
        float moving = distance / Length;
        if (reverseMove) {
            transform.position = Vector3.Lerp(endPoint.transform.position, startPoint.transform.position, moving);
        } else {
            transform.position = Vector3.Lerp(startPoint.transform.position, endPoint.transform.position, moving);
        }
        if ((Vector3.Distance(transform.position, endPoint.transform.position) == 0.0f ||
            Vector3.Distance(transform.position, startPoint.transform.position) == 0.0f)) {
            if (reverseMove) {
                reverseMove = false;
            } else {
                reverseMove = true;
            }
            startTime = Time.time;
        }
    }
}