using System.Collections;
using UnityEngine;

public class LedgeClimbing : MonoBehaviour
{
	public Movement Player;
	public ObjPhysicsSystem objInt;
	public float climbSpeed = 0.5f;
	public float ledgeDetectionLength;
	public float ledgeSphereCastRadius;
	public float upClimb;
	public float forwardClimb;
	public LayerMask ledgeLayer;
	public bool canClimb;
	public bool isClimbing;
	public bool ledgeDetected;
	public RaycastHit climbingPoint;
	public RaycastHit ledgeHit;
	private Camera cam;

	private void Start()
	{
		cam = Camera.main;

	}
	private void Update()
	{
		if (!Player.isGrounded && objInt.currentlyPickedUpObject == null && !isClimbing)
			CheckLedge();

	}

	private void CheckLedge()
	{

		if (Physics.SphereCast(transform.position, ledgeSphereCastRadius, Vector3.up + transform.forward, out var ledgeHit, ledgeDetectionLength, ledgeLayer))
		{

            ledgeDetected = true;

            if (Physics.Raycast(ledgeHit.point + (cam.transform.forward * forwardClimb) + (upClimb * Vector3.up), Vector3.down, out var climbingPoint, 3f))
			{

				StartCoroutine(DoClimb(climbingPoint.point, climbSpeed));

			}

		}
		else
		{
			isClimbing = false;
			ledgeDetected = false;
		}
	}

	private IEnumerator DoClimb(Vector3 targetPos, float duration)
	{
		
		float time = 0;
		Vector3 startPos = transform.position;


		while (time < duration)
		{
			isClimbing = true;

			transform.position = Vector3.Lerp(startPos, targetPos, time / duration);


			Vector3 direction = (targetPos - transform.position);
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 100f);

			time += Time.deltaTime;
			yield return null;
			isClimbing = false;
			
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(ledgeHit.point, ledgeSphereCastRadius);
		Gizmos.DrawSphere(climbingPoint.point, 1);
	}

}

