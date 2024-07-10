using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public HandState handState;
	public Animator animator;
	public LedgeClimbing ledge;
	private ObjPhysicsSystem pickUp;
	private Movement player;
	private CrossHair crosshair;
	private DialogueUI dialogueUI;
	public enum HandState
	{
		None,
		Slap
	}

	private void Start()
	{
		crosshair = Camera.main.GetComponent<CrossHair>();
		player = GameManager.Instance.player;
		pickUp = GameManager.Instance.player.GetComponent<ObjPhysicsSystem>();
		dialogueUI = GameManager.Instance.dialogueUI;
		handState = HandState.None;
	}

	private void Update()
	{
		if (dialogueUI.inResponses)
		{
			animator.SetBool("Speaking", true);
		}
		else
		{
			animator.SetBool("Speaking", false);
		}

		switch (handState)
		{
			case HandState.None:
				animator.SetBool("EffectNone", true);
				animator.SetBool("EffectAttack", false);
				HandsNone();
				break;
			case HandState.Slap:
				animator.SetBool("EffectAttack", true);
				animator.SetBool("EffectNone", false);
				break;
		}
	}

	private void HandsNone()
	{
		if (player.isMoving)
		{
			animator.SetFloat("speed", 2);
		}
		else
		{
			animator.SetFloat("speed", 1);
		}

		//Hidden
		if (pickUp.holdingObject)
		{
			animator.SetBool("Hidden", true);
		}
		else
		{
			animator.SetBool("Hidden", false);
		}

		//Throwing
		if (pickUp.throwingObject)
		{
			animator.SetTrigger("Throwing");
		}
		else
		{
			animator.ResetTrigger("Throwing");
		}

		//Running
		if (player.isMoving && player.isSprinting)
		{
			animator.SetBool("Running", true);
		}
		if (!player.isSprinting || player.isSprinting && !player.isMoving || player.isGrounded == false)
		{
			animator.SetBool("Running", false);
		}

		//Falling
		if (player._Velocity.y < -15)
		{
			animator.SetBool("Falling", true);
		}
		else
		{
			animator.SetBool("Falling", false);
		}

		if (ledge.isClimbing)
		{
			animator.SetBool("Climbing", true);

		}
		else
		{
			animator.SetBool("Climbing", false);
		}
	}

	private void HandsSlap()
	{

	}
}
