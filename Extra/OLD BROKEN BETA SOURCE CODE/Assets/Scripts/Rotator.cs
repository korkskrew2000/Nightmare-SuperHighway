using UnityEngine;

public class Rotator : MonoBehaviour {
    public Vector3 rotateAxis;
    public float rotateSpeed;
    public void SetRotationSpeed(float speed) {
        rotateSpeed = speed;
    }
    void Update() {
        transform.rotation *= Quaternion.AngleAxis(rotateSpeed * Time.deltaTime, rotateAxis);
    }
}
