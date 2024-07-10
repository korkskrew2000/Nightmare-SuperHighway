using UnityEngine;

public class RandomChanceAppear : MonoBehaviour {
    [Range(1.0f, 0.1f)]
    public float chance;
    GameObject appearItem;

    // Start is called before the first frame update
    void Start() {
        appearItem = this.transform.GetChild(0).gameObject;

        if (Random.value > chance) {
            appearItem.SetActive(true);
        } else {
            appearItem.SetActive(false);
        }
    }
}
