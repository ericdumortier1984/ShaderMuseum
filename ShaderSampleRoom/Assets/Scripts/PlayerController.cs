using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private float speed = 5f;
	[SerializeField] private float mouseSensitivity = 0.1f;
	[SerializeField] private Transform cameraPivot;

	float yVelocity;
	float gravity = -9.8f;
	float xRotation = 0f;

	bool canControl = true;

	CharacterController controller;
	PlayerInputActions input;

	Vector2 moveInput;
	Vector2 lookInput;

	void Awake()
	{
		input = new PlayerInputActions();
	}

	void OnEnable()
	{
		input.Enable();

		input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
		input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

		input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
		input.Player.Look.canceled += ctx => lookInput = Vector2.zero;
	}

	void OnDisable()
	{
		input.Disable();
	}

	void Start()
	{
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (canControl)
		{
			Move();
			Look();
		}
	}

	public void EnableControl(bool value)
	{
		canControl = value;

		if (!value)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	void Move()
	{
		Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

		if (controller.isGrounded && yVelocity < 0)
			yVelocity = -2f;

		yVelocity += gravity * Time.deltaTime;

		Vector3 velocity = move * speed;
		velocity.y = yVelocity;

		controller.Move(velocity * Time.deltaTime);
	}

	void Look()
	{
		float mouseX = lookInput.x * mouseSensitivity;
		float mouseY = lookInput.y * mouseSensitivity;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -80f, 80f);

		cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		transform.Rotate(Vector3.up * mouseX);
	}
}
