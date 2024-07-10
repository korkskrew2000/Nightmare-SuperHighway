using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    public AudioSource track1;
    public AudioMixerGroup mixerGroup;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        track1 = gameObject.GetComponent<AudioSource>();
        track1.loop = true;
        track1.outputAudioMixerGroup = mixerGroup;
    }
    public void FadeTrack(AudioClip newClip, float volume)
    {
        if(track1.clip == newClip)
        {
            return;
        }
            track1.Stop();
            track1.clip = newClip;
            track1.volume = volume;
            track1.Play();
    }

}
