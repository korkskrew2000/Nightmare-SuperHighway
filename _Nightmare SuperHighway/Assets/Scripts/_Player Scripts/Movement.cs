using UnityEngine;
/// <summary>
/// Player Input, Controller Movement, Ground Check, Run, Jump and Crouch.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
	#region Public
	public GameObject lastHit;
	public LedgeClimbing ledgeClimb;
	//Moving
	public float currentSpeed;
	public float runningSpeed = 15f;
	public float crouchSpeed = 5f;
	//Walking
	public float walkingSpeed = 10f;
	public float walkingAcceleration = 5f;
	//Jumping
	public float jumpHeight = 2f;
	public float gravity = 30f;
	public float maxFallingSpeed = 30f;
	#endregion

	#region Private
	//Briefly allows player to jump after leaving the ground
	private float jumpTimer = 0.25f;
	private float jumpReset;
	private bool nolongerCrouch = false;
	private bool jump;
	private bool canJumpToggle;
	private LayerMask groundMask = 1 << 6 | 1 << 9 | 1 << 12;
	private LayerMask ceilingMask = 1 << 7;
	private Vector2 currentDir = Vector2.zero;
	private Vector2 currentDirVelocity = Vector2.zero;
	#endregion

	#region Serialized
	[SerializeField] private Transform ceilingCheck;
	#endregion

	#region ReadOnly
	private readonly float minimumSpeed = 0.1f;
	private readonly float groundDistance = 0.25f;
	private readonly float normalHeight = 3;
	private readonly float crouchHeight = 1.5f;
	private readonly float crouchingStandDistance = 0.5f;
	private readonly float jumpSlope = 90.0f;
	private readonly float controllerSlope = 45.0f;
	private readonly float moveSmoothTime = 0.3f;
	#endregion

	#region Hidden
	//Need to be public for bounce and launch pad to work
	[HideInInspector] public Vector3 _Velocity = Vector3.zero;
	 public bool isGrounded;
	//Need to be public for headbobbing to work
	[HideInInspector] public Vector2 targetDir = Vector2.zero;
	[HideInInspector] public bool isMoving;
	[HideInInspector] public bool isSprinting;
	[HideInInspector] public bool isCrouching;
	[HideInInspector] public bool cantStand;
	[HideInInspector] public RaycastHit hitGround;
	[HideInInspector] public CharacterController controller;
	public bool jumpHeld;
	#endregion

	public void Start()
	{
		controller = GetComponent<CharacterController>();
		controller.slopeLimit = controllerSlope;
	}

	private void Update()
	{
		//Player input

		if (Input.GetButtonDown("Jump") && jumpReset > 0f && canJumpToggle && !ledgeClimb.isClimbing)
		{
			jump = true;
			isGrounded = false;
		}
		if (Input.GetButton("Jump"))
		{
			jumpHeld = true;
		}
		if (Input.GetButtonUp("Jump"))
		{
			jumpHeld = false;
		}

		isSprinting = Input.GetButton("Sprint");

		if (Input.GetButton("Crouch"))
		{
			isCrouching = true;
			if (cantStand)
			{
				canJumpToggle = false;
				isCrouching = true;
			}
		}
		else
		{
			isCrouching = false;
			canJumpToggle = true;
		}

		if (Input.GetButtonUp("Crouch") && !isCrouching)
		{
			nolongerCrouch = true;
		}
	}
	public void FixedUpdate()
	{
		if (GameManager.Instance.controllable)
		{
			//Movement using Unity's built in Character Controller
			targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			targetDir.Normalize();
			currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
			Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed;
			_Velocity.y -= gravity * Time.deltaTime;
			Vector3 vectorjump = transform.up * _Velocity.y;
			Vector3 Move = vectorjump + velocity;
			controller.Move(Move * Time.deltaTime);

			CheckGround();
			CheckMovement();
			if (!ledgeClimb.isClimbing)
			{
				HandleJump();
				HandleRun();
				HandleCrouch();
			}
			else
			{
				return;
			}
		}

	}

	//Check if player is on ground
	private void CheckGround()
	{
		if (Physics.CheckCapsule(controller.bounds.center, new Vector3(controller.bounds.center.x,
			controller.bounds.min.y - 0.1f, controller.bounds.center.z), groundDistance, groundMask)
			|| Physics.SphereCast(transform.position + controller.center, controller.radius, Vector3.down,
			out hitGround, 1.2f, groundMask) && jump == false)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		//=================
		if (isGrounded)
		{
			_Velocity.y = -2f;
			controller.slopeLimit = controllerSlope;
			jumpReset = jumpTimer;
		}
		else
		{
			controller.slopeLimit = jumpSlope;
			jumpReset -= Time.deltaTime;
		}
		//Limits player's falling speed so it wont increase indefinitely
		if (_Velocity.y < -maxFallingSpeed)
		{
			_Velocity.y = -maxFallingSpeed;
		}
		//Reset extra jump time
		if (jumpReset <= 0f && isGrounded)
		{
			jumpReset = jumpTimer;
		}
	}

	private void HandleJump()
	{
		if (jump == true && !isCrouching)
		{
			jumpReset -= 1f;
			_Velocity.y = Mathf.Sqrt(2f * jumpHeight * gravity);
			currentSpeed = runningSpeed;
			jump = false;
		}
	}

	private void HandleRun()
	{
		if (isSprinting == true && isMoving == true)
		{
			if (currentSpeed < runningSpeed && !isCrouching)
			{
				currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, 5 * Time.deltaTime);
			}
		}

		//Player cannot accelerate past maximum running speed
		if (currentSpeed >= runningSpeed)
		{
			currentSpeed = runningSpeed;
		}

		//Speed goes back to normal if player is not running
		if (isSprinting == false && isMoving == false)
		{
			if (currentSpeed > minimumSpeed)
			{
				currentSpeed = Mathf.Lerp(currentSpeed, minimumSpeed, 5 * Time.deltaTime);
			}
		}
	}

	private void HandleCrouch()
	{
		cantStand = Physics.CheckSphere(ceilingCheck.position, crouchingStandDistance, ceilingMask);
		if (isCrouching == true && isGrounded || cantStand)
		{
			/*I don't know why, but doing it this way is the only way I've found to make the character
            not get stuck in the ground after standing up.*/
			controller.height = Mathf.Lerp(controller.height, crouchHeight, Time.deltaTime * 200);
			currentSpeed = crouchSpeed;
			jump = false;

			//Player remains crouched if there is no room to stand up.
			if (cantStand)
			{
				currentSpeed = Mathf.Lerp(currentSpeed, crouchSpeed, 20 * Time.deltaTime);
				isCrouching = true;
				canJumpToggle = false;
			}
		}

		//Back to normal height
		if (isCrouching == false && !cantStand)
		{
			controller.height = Mathf.Lerp(controller.height, normalHeight, Time.deltaTime * 200);
		}

		//Speed and Crouch reset
		if (nolongerCrouch == true && !cantStand)
		{
			currentSpeed = walkingSpeed;
			canJumpToggle = true;
			nolongerCrouch = false;
		}
	}

	private void CheckMovement()
	{
		if (targetDir != Vector2.zero)
		{
			isMoving = true;
			if (!isSprinting)
			{
				currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, walkingAcceleration * Time.deltaTime);
			}

		}
		else
		{
			isMoving = false;
			currentSpeed = Mathf.Lerp(currentSpeed, minimumSpeed, 5 * Time.deltaTime);
		}
	}



}