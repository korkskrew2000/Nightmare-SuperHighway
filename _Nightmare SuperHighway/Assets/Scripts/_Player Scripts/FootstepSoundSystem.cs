using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSoundSystem : MonoBehaviour
{
	float testT;
	bool playedSound;
	bool askForSound;
    AudioSource audioSource;
	public AudioClip[] walkGeneric;
	Movement player;

	void Start()
	{
		player = GameManager.Instance.player;
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		player = GameManager.Instance.player;
    }

	void FixedUpdate()
	{
        
        if (player.isMoving && player.isGrounded && walkGeneric.Length != 0 && GameManager.Instance.controllable && !player.isCrouching)
		{
            testT = transform.localPosition.y;
            if (testT <= 1.25f && playedSound == false && askForSound == true)
			{
				playedSound = true;
            }
            if (testT >= 1.29f)
            {
				askForSound = true;
            }
		}
		if(playedSound == true && askForSound == true)
		{
			PlayRandomStep();
        }
	}

	void PlayRandomStep()
	{
		askForSound = false;
		audioSource.clip = walkGeneric[Random.Range(0, walkGeneric.Length)];
		audioSource.pitch = Random.Range(0.8f, 1.2f);
		audioSource.PlayOneShot(audioSource.clip);
        playedSound = false;
    }
}
