using UnityEngine;

public class VoidNet : MonoBehaviour {
    public Transform voidNetRespawnPoint;
    public bool AutoFindSpawn = true;
    GameObject player;
    CharacterController controller;


    public void Start() {
        player = GameManager.Instance.player.gameObject;
        controller = GameManager.Instance.player.GetComponent<CharacterController>();
        if (AutoFindSpawn) {
            Invoke(nameof(RespawnNetFindSpawn), 0.3f);
        }
    }

    void RespawnNetFindSpawn() {
        voidNetRespawnPoint.transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other) {
        //Controller has to be disabled for a brief moment for the teleportation to work.
        controller.enabled = false;
        player.transform.position = voidNetRespawnPoint.transform.position;
        controller.enabled = true;
    }
}
