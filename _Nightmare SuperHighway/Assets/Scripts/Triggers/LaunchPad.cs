using UnityEngine;

public class LaunchPad : MonoBehaviour {
    /// <summary>
    /// Launches the player upwards the specified amount indefinetely.
    /// </summary>
    public float launchPower = 10f;
    public bool debugVisualOn;
    Movement Player => GameManager.Instance.player;
    Collider Xcollider => this.GetComponent<Collider>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Player._Velocity.y = launchPower;
        }
    }
    void OnDrawGizmos() {
        if (debugVisualOn) {
            //If there is no collider added inside inspector it won't try to draw anything.
            if (Xcollider != null) {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(Xcollider.bounds.center, Xcollider.bounds.size);
            }
        }
    }
}
