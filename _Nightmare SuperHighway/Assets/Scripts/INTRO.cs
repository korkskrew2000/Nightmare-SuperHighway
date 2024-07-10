using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class INTRO : MonoBehaviour
{
    public VideoPlayer video;
    public AudioSource audioSource;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        video.controlledAudioTrackCount = 1;
        video.EnableAudioTrack(0, true);
        video.SetTargetAudioSource(0, audioSource);
        video.Play();


    }

    public IEnumerator Start(){
        yield return new WaitForSeconds(16);
        SceneManager.LoadScene(1);
    }

}