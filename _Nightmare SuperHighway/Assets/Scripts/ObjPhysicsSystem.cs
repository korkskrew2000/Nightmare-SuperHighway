using System.Collections;
using UnityEngine;

public class ObjPhysicsSystem : MonoBehaviour
{
	#region Public Variables
	[Header("Physics")]
	public MouseLooker mouselooker;
	public float sphereCastRadius = 0.5f;
	public int interactableLayerIndex;
	public GameObject lookObject;
	public GameObject currentlyPickedUpObject;
	public float rotationSpeed = 100f;
	public float lookBreak = 60f;
	public bool holdingObject = false;
	public bool throwingObject = false;
	[SerializeField] private Transform pickupParent;
	public float currentSpeed = 0f;
	#endregion

	#region Private Variables
	//Physics
	private Camera mainCamera;
	private CrossHair crosshair;
	private PhysicsObject physicsObject;
	private Rigidbody pickupRB;
	private readonly float minSpeed = 0;
	private readonly float maxSpeed = 700f;
	private readonly float maxDistance = 5f;
	private float currentDist = 0f;
	private Quaternion lookRot;
	bool canDrop;
	///////////////////////////////////
	#endregion

	private void Start()
	{
		mainCamera = Camera.main;
		crosshair = mainCamera.GetComponent<CrossHair>();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(pickupParent.position, 0.5f);
	}

	private void Update()
	{
		if (crosshair.crosshairState == CrossHair.State.Interactable)
		{
			lookObject = crosshair.normalHit.collider.transform.gameObject;
		}
		else
		{
			lookObject = null;
		}

		if (canDrop == false)
		{
			StartCoroutine(Droppable());
		}

		if (Input.GetButtonDown("Interact") && lookObject != null)
		{
			if (currentlyPickedUpObject == null)
			{
				PickUpObject();
			}
			else
			{
				BreakConnection();
			}
		}

        if (Input.GetButtonDown("Interact") && canDrop && currentlyPickedUpObject != null)
        {
                BreakConnection();
        }

        if (Input.GetButtonDown("InteractThrow") && currentlyPickedUpObject != null)
		{
			throwingObject = true;
			pickupRB.AddForce(mainCamera.transform.forward * physicsObject.throwForce);
			BreakConnection();
		}
	}

	IEnumerator Droppable()
	{
		yield return new WaitForSeconds(0.1f);
		canDrop = true;

	}

	private void FixedUpdate()
	{
		if (currentlyPickedUpObject != null)
		{
			holdingObject = true;
			currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
			currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, currentDist * maxDistance);
			currentSpeed *= Time.fixedDeltaTime;
			Vector3 direction = pickupParent.position - pickupRB.position;
			pickupRB.velocity = direction.normalized * currentSpeed;
			//Rotation
			lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
			lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
			pickupRB.MoveRotation(lookRot);

			if (physicsObject.isSmallPhysicsObject == false)
			{
				if (mouselooker.xRotation >= lookBreak)
				{
					BreakConnection();
				}
			}
		}
	}

	
	/*
	public void DetermineLookObject()
	{
		if (lookObject != null)
		{
			Interactable look = lookObject.GetComponentInChildren<Interactable>();
			if (holdingObject == false && look.interactionType == Interactable.InteractionType.PhysicsObject)
			{
				PickUpObject();
			}

			if (look.interactionType == Interactable.InteractionType.Collectible)
			{
				look.GetCollectible();
			}

			if (look.interactionType == Interactable.InteractionType.Door)
			{
				look.DoorInteract();
			}
		}
	}*/

	public void BreakConnection()
	{
		physicsObject.isObjDropped = true;
		pickupRB.velocity = Vector3.zero;
		pickupRB.angularVelocity = Vector3.zero;
		pickupRB.constraints = RigidbodyConstraints.None;
		currentlyPickedUpObject = null;
		physicsObject.pickedUp = false;
		currentDist = 0;
		StartCoroutine(ResetHold());
		StartCoroutine(ResetThrow());
		canDrop = true;
	}

	public void PickUpObject()
	{
		canDrop = false;
		physicsObject = lookObject.GetComponent<PhysicsObject>();
		lookBreak = physicsObject.physicsLookRotationBreak;
		currentlyPickedUpObject = lookObject;
		pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
		pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
		physicsObject.pickupObjects = this;
		StartCoroutine(physicsObject.PickUp());
	}

	public IEnumerator ResetHold()
	{
		yield return new WaitForSeconds(0.5f);
		holdingObject = false;
	}

	public IEnumerator ResetThrow()
	{
		yield return new WaitForSeconds(0.5f);
		throwingObject = false;
	}
}