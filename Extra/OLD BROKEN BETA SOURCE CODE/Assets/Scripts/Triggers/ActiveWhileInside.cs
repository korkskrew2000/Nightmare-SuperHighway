using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhileInside : MonoBehaviour
{
    public GameObject objectactive;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            objectactive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            objectactive.SetActive(false);
        }
    }
}
