using UnityEngine;

public class ActivateMusic : MonoBehaviour {
    public bool SkipIntro;

    private void OnTriggerEnter(Collider other) {
        if (SkipIntro) {
            FindFirstObjectByType<MusicManager>().loop.Play();
        } else {
            FindFirstObjectByType<MusicManager>().intro.Play();
        }
    }
}