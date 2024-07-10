using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
    public bool playAtStart;
    public bool loopOnly;
    public AudioSource intro;
    public AudioSource loop;
    public AudioMixerGroup mixerGroup;

    private void Awake() {
        intro.outputAudioMixerGroup = mixerGroup;
        loop.outputAudioMixerGroup = mixerGroup;
    }

    private void Start() {
        if (playAtStart) {
            if (!loopOnly) {
                StartCoroutine(PlayLoop());
            } else {
                StartCoroutine(OnlyLoop());
            }
        }
    }

    IEnumerator PlayLoop() {
        intro.Play();
        loop.PlayDelayed(intro.clip.length);
        yield return null;
    }

    IEnumerator OnlyLoop() {
        loop.Play();
        yield return null;
    }
}
