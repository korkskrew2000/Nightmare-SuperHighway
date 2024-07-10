using UnityEngine;

public class SpeedModifier : MonoBehaviour {
    public float speedChange = 3f;
    [Space(5)]
    public float gravityChange;
    public float jumpChange;
    public bool debugVisualOn;
    Collider Xcollider => this.GetComponent<Collider>();

    GameObject player;
    Movement movement;
    float normalSpeed;
    float normalGravity;
    float normalJumpHeight;

    void Start() {
        player = GameManager.Instance.player.gameObject;
        movement = GameManager.Instance.player.GetComponent<Movement>();
        normalSpeed = movement.currentSpeed;
        normalGravity = movement.gravity;
        normalJumpHeight = movement.jumpHeight;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) //So this only works when player crosses into the trigger
        {
            movement.currentSpeed = speedChange;
            movement.jumpHeight = jumpChange;
            movement.gravity = gravityChange;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            movement.currentSpeed = normalSpeed;
            movement.jumpHeight = normalJumpHeight;
            movement.gravity = normalGravity;
        }
    }

    void OnDrawGizmos() {
        if (debugVisualOn) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Xcollider.bounds.center, Xcollider.bounds.size);
        }
    }
}
