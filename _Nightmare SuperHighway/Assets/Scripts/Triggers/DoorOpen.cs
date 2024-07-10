using UnityEngine;

public class DoorOpen : MonoBehaviour {
    [SerializeField] Animator anim;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            anim.SetBool("doorOpen", true);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            anim.SetBool("doorStay", true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            anim.SetBool("doorStay", false);
            anim.SetBool("doorOpen", false);
        }
    }
}