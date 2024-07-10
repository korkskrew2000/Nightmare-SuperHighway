using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	#region PhysicsObject
	[Range(0f, 90f)]
	public float physicsLookRotationBreak = 60f;
	public bool isSmallPhysicsObject = false;
	public bool playSoundOnDrop;
	public float dropSoundForce = 5f;
	public AudioClip dropSound;
	public bool isBreakable = true;
	public float throwForce = 100f;
	public float dropBreakSpeedForce = 10f;
	public AudioClip breakSound;
	public GameObject destroyedPrefab;
	#region PhysicsObject Private
	[HideInInspector] public ObjPhysicsSystem pickupObjects;
	#endregion

	#region PhysicsObject Debug
	[Space(15)]
	public float lastDropImpactForce;
	public bool isObjDropped;
	#endregion


	public float timeTillDestroyed = 2f;
	[HideInInspector] public float waitOnPickup = 0.2f;
	[HideInInspector] public float breakForce = 35f;
	[HideInInspector] public bool pickedUp = false;
	private ParticleSystem particles;
	private MeshRenderer mesh;
	private Collider coll;
	private AudioSource audios;
	#endregion

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
	}


	public IEnumerator PickUp()
	{
		yield return new WaitForSecondsRealtime(waitOnPickup);
		pickedUp = true;
	}

	public IEnumerator DestroyPhysicsObject()
	{
		if (destroyedPrefab != null)
		{
			Instantiate(destroyedPrefab, transform.position, transform.rotation);
			StartCoroutine(DestroyObject());
		}
		else
		{
			yield return null;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (pickedUp)
		{
			if (collision.relativeVelocity.magnitude > breakForce)
			{
				pickupObjects.BreakConnection();
			}
		}

		if (isObjDropped && audios != null)
		{
			lastDropImpactForce = collision.relativeVelocity.magnitude;
			if (collision.relativeVelocity.magnitude > dropSoundForce && collision.relativeVelocity.magnitude < dropBreakSpeedForce)
			{
				audios.clip = dropSound;
				audios.Play();
				Debug.Log(gameObject.name + " dropped and played sound at collision speed: " + collision.relativeVelocity.magnitude);

			}
			else if (collision.relativeVelocity.magnitude > dropBreakSpeedForce && collision.relativeVelocity.magnitude > dropSoundForce)
			{
				audios.clip = breakSound;
				audios.Play();
				Debug.Log(gameObject.name + " destroyed at collision speed: " + collision.relativeVelocity.magnitude);
				if (isBreakable) StartCoroutine(DestroyPhysicsObject());
			}
			else
			{
				Debug.Log(gameObject.name + " dropped silently at collision speed: " + collision.relativeVelocity.magnitude);
				isObjDropped = false;
			}
		}
	}


	//Shared Object destroy command
	public IEnumerator DestroyObject()
	{
		mesh.enabled = false;
		coll.enabled = false;
		yield return new WaitForSeconds(timeTillDestroyed);
		Destroy(this.gameObject);
	}
}