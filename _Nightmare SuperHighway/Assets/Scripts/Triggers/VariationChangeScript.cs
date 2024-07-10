using System.Collections;
using UnityEngine;

public class VariationChangeScript : MonoBehaviour {
    bool var1isActive = false;
    bool var2isActive = false;
    bool isChanged = true;
    public GameObject Variation1, Variation2;
    public Collider Xcollider => this.GetComponent<Collider>();

    void Start() {
        StartCoroutine(StartingVariation());
        StartCoroutine(VariationChange());
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Xcollider.bounds.center, Xcollider.bounds.size);
    }

    private void OnTriggerEnter(Collider other) {
        if (!isChanged) {
            if (other.CompareTag("Player")) {
                isChanged = true;
                StartCoroutine(VariationChange());
            }
        }
    }

    //Choosing a room at random when started
    public IEnumerator StartingVariation() {
        if (Random.Range(0, 2) == 0) {
            var1isActive = true;
        } else {
            var2isActive = true;
        }
        yield return null;
    }

    //Rooms toggled on and off
    public IEnumerator VariationChange() {
        if (isChanged) {
            var1isActive = !var1isActive;
            var2isActive = !var2isActive;
            if (var1isActive == true) {
                Variation1.gameObject.SetActive(true);
            } else {
                Variation1.gameObject.SetActive(false);
            }
            if (var2isActive == true) {
                Variation2.gameObject.SetActive(true);
            } else {
                Variation2.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(5f);
            isChanged = false;
        }
    }
}
