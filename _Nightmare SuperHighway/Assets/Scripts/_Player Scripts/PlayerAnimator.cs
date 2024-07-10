using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public Animator animator;
	public LedgeClimbing ledge;
    public ObjPhysicsSystem pickUp;
	private Movement player;
	private CrossHair crosshair;

	private void Start()
	{
		crosshair = Camera.main.GetComponent<CrossHair>();
		player = GameManager.Instance.player;
	}

	private void Update()
	{
        if (!player.isMoving && !player.isSprinting)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }

        if (player.isMoving && !player.isSprinting && !player.isCrouching)
		{
            animator.SetBool("Walking", true);
        }
		else
		{
            animator.SetBool("Walking", false);
        }

		//Hidden
		if (pickUp.holdingObject || player.isGrounded == false)
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

		if (ledge.isClimbing)
		{
			animator.SetBool("Climbing", true);

		}
		else
		{
			animator.SetBool("Climbing", false);
		}
	}
}
