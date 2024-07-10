using UnityEngine;

public class ShadowGroup : MonoBehaviour {
    Movement Player => GameManager.Instance.player;
    Vector3 Form;
    readonly float x;
    readonly float z;

    private void Start() {
        Form = this.gameObject.transform.position;
    }

    void Update() {
        Form.x = Player.transform.position.x;
        Form.z = Player.transform.position.z;
        this.gameObject.transform.position = Form;
    }
}
