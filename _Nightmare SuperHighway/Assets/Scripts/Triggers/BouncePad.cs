using UnityEngine;
/// <summary>
/// Bounces the player up to the same position they fell from a certain number of times.
/// </summary>
public class BouncePad : MonoBehaviour {
    public float maxNumberOfBounces = 3f;
    public float extraHeight = 2f;
    public float startAffectingHeight = 10f;
    public float consecutiveBounceFallOff = 2;
    public Vector3 fallStartPos;
    public float oldVal;
    [Space(5)]
    public bool debugVisualOn;
    float extraHeighReset;
    float currentBounces;
    bool canBounce = true;
    float lastCallTime;
    Collider Xcollider => this.GetComponent<Collider>();
    Movement Player => GameManager.Instance.player;

    private void Start() {
        extraHeighReset = extraHeight;
    }

    void LateUpdate() {
        if (Player.isGrounded && Time.time - lastCallTime >= 0.2f) {
            ResetFunction();
        }

        if (currentBounces >= maxNumberOfBounces) {
            canBounce = false;
        }
    }

    void ResetFunction() {
        fallStartPos.y = Player.transform.position.y;
        canBounce = true;
        extraHeight = extraHeighReset;
        currentBounces = 0f;
        oldVal = 0f;
    }

    private void OnTriggerEnter(Collider other) {
        if (canBounce && fallStartPos.y > startAffectingHeight && other.gameObject.CompareTag("Player")) {
            Player._Velocity.y = fallStartPos.y + extraHeight;
            currentBounces++;
        }
        if (currentBounces > oldVal) {
            extraHeight -= consecutiveBounceFallOff;
            oldVal = currentBounces;
        }
    }

    void OnDrawGizmos() {
        if (debugVisualOn) {
            //If there is no collider added inside inspector it won't try to draw anything.
            if (Xcollider != null) {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(Xcollider.bounds.center, Xcollider.bounds.size);
            }
        }
    }
}
