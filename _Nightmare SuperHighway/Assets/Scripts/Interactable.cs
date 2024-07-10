using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{

	public enum InteractionType
	{
		Door,
		Collectible,
	}
	public InteractionType interactionType;


	#region Door
	public bool doorIsOpen;
	#region Door Private
	private Animator anim;
	#endregion
	#endregion

	#region Collectible
	public bool collectOnTouch = false;
	public bool playSoundonCollected = false;
	public AudioClip collectSound;
	public bool playParticleonCollected = false;
	public bool isSpinning = false;
	public float spinSpeed = 50f;
	#region Collectible Private
	#endregion
	#endregion
	public float timeTillDestroyed = 2f;

	#region Private
	private Collider coll;
	private AudioSource audios;
	#endregion

	private ParticleSystem particles;
	private MeshRenderer mesh;

	private void Start()
	{
		mesh = GetComponent<MeshRenderer>();
		coll = GetComponent<Collider>();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		gameObject.layer = LayerMask.NameToLayer("Interactable");

		if (audios != null)
		{
			audios.spatialBlend = 1;
		}

		switch (interactionType)
		{
			case InteractionType.Door:
				collectOnTouch = false;
				playSoundonCollected = false;
				playParticleonCollected = false;
				isSpinning = false;
				anim = GetComponent<Animator>();
				break;

			case InteractionType.Collectible:
				if (playParticleonCollected) particles = GetComponentInChildren<ParticleSystem>();
				if (playSoundonCollected) audios = GetComponent<AudioSource>();
				if (isSpinning) StartCoroutine(Spin());
				break;
		}
	}


	#region Door *************************************************************************************************************************
	public void DoorInteract()
	{
		doorIsOpen = !doorIsOpen;
		if (doorIsOpen)
		{
			anim.SetBool("Open", true);
		}
		else
		{
			anim.SetBool("Open", false);
		}
	}
	#endregion


	#region Collectible ******************************************************************************************************************
	public void GetCollectible()
	{
		if (playParticleonCollected)
		{
			particles.Play();
		}
		if (playSoundonCollected)
		{
			audios.pitch = Random.Range(0.6f, 1.1f);
			audios.PlayOneShot(collectSound);
		}
		StartCoroutine(DestroyObject());
	}

	public void OnTriggerEnter(Collider other)
	{
		if (collectOnTouch && other.gameObject.CompareTag("Player"))
		{
			GetCollectible();
		}
	}

	private IEnumerator Spin()
	{
		while (true)
		{
			transform.rotation *= Quaternion.AngleAxis(spinSpeed * Time.deltaTime, Vector3.forward);
		}
	}
	#endregion


	//Shared Object destroy command
	public IEnumerator DestroyObject()
	{
		mesh.enabled = false;
		coll.enabled = false;
		yield return new WaitForSeconds(timeTillDestroyed);
		Destroy(this.gameObject);
	}
}