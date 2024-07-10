using System.Collections;
using UnityEngine;

public class PlaySoundTrigger : MonoBehaviour
{
	AudioSource audioSource;
	public float timeTillRemoved = 2f;
	bool notPlayed = true;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && notPlayed)
		{
			notPlayed = false;
			StartCoroutine(PlaySound());
		}
	}

	public IEnumerator PlaySound()
	{
		audioSource.PlayOneShot(audioSource.clip);
		yield return new WaitForSeconds(timeTillRemoved);
		Destroy(this.gameObject);
	}
}