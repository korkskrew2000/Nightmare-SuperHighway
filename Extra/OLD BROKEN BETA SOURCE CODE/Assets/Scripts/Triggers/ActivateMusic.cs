using UnityEngine;

public class ActivateMusic : MonoBehaviour {
    public bool SkipIntro;

    private void OnTriggerEnter(Collider other) {
        if (SkipIntro) {
            FindObjectOfType<MusicManager>().loop.Play();
        } else {
            FindObjectOfType<MusicManager>().intro.Play();
        }
    }
}