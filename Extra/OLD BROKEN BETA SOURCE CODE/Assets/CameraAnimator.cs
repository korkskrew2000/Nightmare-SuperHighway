using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
    Animator anim;
    public LedgeClimbing ledgeClimbing;
    Camera cam;
    public MouseLooker mouselooker;
    public bool cameraIsNotPlayingAnimation = true;
    bool stopAnim;

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        anim.enabled = false;
    }

    void Update()
    {
        if (ledgeClimbing.isClimbing)
        {
            anim.enabled = true;
            anim.SetBool("Climbing", true);
            cameraIsNotPlayingAnimation = false;

        }
        else
        {
            anim.SetBool("Climbing", false);
            cameraIsNotPlayingAnimation = true;
            anim.enabled = false;
        }
    }

    void StopCamAnims()
	{
        if (stopAnim)
		{
            cameraIsNotPlayingAnimation = true;
            stopAnim = false;
        }
	}
}
