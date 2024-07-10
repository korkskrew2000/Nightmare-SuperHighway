using UnityEngine;

public class HandSway : MonoBehaviour {
    public float swayAmount;
    public float maximumSwayAmount;
    public float smoothAmount;
    MouseLooker mouse;
    Vector3 initialPosition;

    void Start() {
        mouse = Camera.main.GetComponent<MouseLooker>();
        initialPosition = transform.localPosition;
    }

    void Update() {
        HandsMoveWithCamera();
    }

    void HandsMoveWithCamera() {
        float moveX = mouse.mouseX * swayAmount;
        float moveY = mouse.mouseY * swayAmount;
        moveX = Mathf.Clamp(moveX, -maximumSwayAmount, maximumSwayAmount);
        moveY = Mathf.Clamp(moveY, -maximumSwayAmount, maximumSwayAmount);
        Vector3 finalPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}