using UnityEngine;

public class GravityModifier : MonoBehaviour {
    [Header("Default = 30")]
    public float gravityChange;
    [Space(10)]
    [Header("Default = 2")]
    public float jumpChange;
    [Space(10)]
    public bool debugVisualOn;
    Collider Xcollider => this.GetComponent<Collider>();

    GameObject player;
    Movement movement;
    float normalGravity;
    float normalJumpHeight;

    void Start() {
        player = GameManager.Instance.player.gameObject;
        movement = GameManager.Instance.player.GetComponent<Movement>();
        normalGravity = movement.gravity;
        normalJumpHeight = movement.jumpHeight;
    }

    void OnDrawGizmos() {
        if (debugVisualOn) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Xcollider.bounds.center, Xcollider.bounds.size);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) //So this only works when player crosses into the trigger
        {
            movement.jumpHeight = jumpChange;
            movement.gravity = gravityChange;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            movement.jumpHeight = normalJumpHeight;
            movement.gravity = normalGravity;
        }
    }
}
