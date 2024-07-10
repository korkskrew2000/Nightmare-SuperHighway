using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerRandom : MonoBehaviour
{
    public VideoClip[] VideoClipArray;
    public VideoPlayer videoPlayer;

    private float timeUntilNextVideo;
    void Awake()
    {
        videoPlayer.Pause();
    }
    void Start()
    {
        timeUntilNextVideo = 0f;


    }


    void Update()
    {
        if (Time.time > timeUntilNextVideo)
        {

            videoPlayer.clip = VideoClipArray[Random.Range(0, VideoClipArray.Length)];


            timeUntilNextVideo = Time.time + (float)videoPlayer.clip.length;

            videoPlayer.Play();
        }
    }

}