using System.Collections.Generic;
using UnityEngine;

public class ActivateProgress : MonoBehaviour {
    public List<GameObject> activateObject = new List<GameObject>();
    public List<GameObject> deactivateObject = new List<GameObject>();
    public Collider Xcollider => this.GetComponent<Collider>();

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Xcollider.bounds.center, Xcollider.bounds.size);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            for (int i = 0; i < activateObject.Count; i++) {
                activateObject[i].SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            for (int i = 0; i < deactivateObject.Count; i++) {
                deactivateObject[i].SetActive(false);
            }
        }
    }
}