using UnityEngine;

public class Teleporter : MonoBehaviour {
    [Header("Attached object will become the teleporter location")]
    public Transform teleporterLocation;
    GameObject player;
    CharacterController controller;

    void Start() {
        player = GameManager.Instance.player.gameObject;
        controller = GameManager.Instance.player.GetComponent<CharacterController>();
        teleporterLocation = this.transform.GetChild(0).transform;
        //Teleporter Location object has to be the child of the object this script is attached to.
    }

    private void OnTriggerEnter(Collider other) {
        //Only works when player touches the teleport.
        if (other.tag == "Player") {
            controller.enabled = false;
            player.transform.position = teleporterLocation.transform.position;
            controller.enabled = true;
        }
    }
}
