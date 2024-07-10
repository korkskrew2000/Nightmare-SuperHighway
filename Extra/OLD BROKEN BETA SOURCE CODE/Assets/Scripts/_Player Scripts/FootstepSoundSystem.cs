using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSoundSystem : MonoBehaviour
{
	AudioSource audioSource;
	public AudioClip[] walkSounds;
	Movement player;
	[SerializeField] float soundCount;
	public float speed;

	void Start()
	{
		player = GameManager.Instance.player;
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = true;
		player = GameManager.Instance.player;
	}

	void FixedUpdate()
	{
		if (player.isMoving && player.isGrounded && walkSounds.Length != 0 && GameManager.Instance.controllable)
		{
			soundCount += player.currentSpeed * Time.deltaTime;
			if (soundCount >= speed)
			{
				soundCount = 0f;
				PlayRandomStep();
			}
		}
		else
		{
			soundCount = 0f;
		}
	}

	void PlayRandomStep()
	{
		audioSource.clip = walkSounds[Random.Range(0, walkSounds.Length)];
		audioSource.pitch = Random.Range(0.7f, 1.8f);
		audioSource.PlayOneShot(audioSource.clip);
	}
}
