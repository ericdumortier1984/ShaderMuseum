using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
	[Header("Interaction Settings")]
	[SerializeField] private CanvasGroup interactCanvas;
	[SerializeField] private CanvasGroup leaveGlassesCanvas;
	[SerializeField] private CanvasGroup resetViewCanvas;
	[SerializeField] private float fadeSpeed = 5f;
	[SerializeField] private float distance = 3f;
	[SerializeField] private Camera playerCamera;

	// Player input actions
	PlayerInputActions input;

	// State variables
	private bool isInteracting = false;
	private float interactTextAlpha = 0f;
	private float leaveGlassesTextAlpha = 0f;
	private float resetViewTextAlpha = 0f;

	// Glasses interface
	private IReleasable currentGlasses;
	private IResettable newView;

	void Awake()
	{
		input = new PlayerInputActions();
	}

	private void Update()
	{
		if (!isInteracting) 
		{
			CheckInteractable();
		}

		LeaveGlasses();
		ResetView();
		FadeOut();
	}

	private void OnEnable()
	{
		input.Enable();
		input.Player.Interact.performed += OnInteract;
	}

	private void OnDisable()
	{
		input.Disable();
		input.Player.Interact.performed -= OnInteract;
	}

	private void OnInteract(InputAction.CallbackContext ctx)
	{
		if (currentGlasses != null)
		{
			currentGlasses.DropGlasses();
			currentGlasses = null;
			isInteracting = false;
			return;
		}

		if (newView != null)
		{
			newView.ResetPassRendererFeature();
			newView = null;
			isInteracting = false;
			return;
		}

		Interact();
	}

	private void Interact()
	{
		Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

		if (Physics.Raycast(ray, out RaycastHit hit, distance))
		{
			var interactable = hit.collider.GetComponent<IInteractable>();

			if (interactable != null)
			{
				interactable.Interact();

				var releasableGlasses = hit.collider.GetComponent<IReleasable>();
				if (releasableGlasses != null)
				{
					currentGlasses = releasableGlasses;
				}

				var resettableView = hit.collider.GetComponent<IResettable>();
				if (resettableView != null)
				{
					newView = resettableView;
				}

				SetInteracting(true);
			}
		}
	}

	private void CheckInteractable()
	{
		Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

		bool hasInteractable = false;

		if (Physics.Raycast(ray, out RaycastHit hit, distance))
		{
			hasInteractable = hit.collider.GetComponent<IInteractable>() != null;
		}

		interactTextAlpha = hasInteractable ? 1f : 0f;
	}

	public void SetInteracting(bool value)
	{
		isInteracting = value;

		if (value)
		{
			interactTextAlpha = 0f;
			leaveGlassesTextAlpha = 0f;
			resetViewTextAlpha = 0f;
		}
	}

	private void FadeOut()
	{
		interactCanvas.alpha = Mathf.Lerp(interactCanvas.alpha, interactTextAlpha, Time.deltaTime * fadeSpeed);
		leaveGlassesCanvas.alpha = Mathf.Lerp(leaveGlassesCanvas.alpha, leaveGlassesTextAlpha, Time.deltaTime * fadeSpeed);
		resetViewCanvas.alpha = Mathf.Lerp(resetViewCanvas.alpha, resetViewTextAlpha, Time.deltaTime * fadeSpeed);
	}

	private void LeaveGlasses()
	{
		bool hasGlasses = currentGlasses != null;

		leaveGlassesTextAlpha = hasGlasses ? 1f : 0f;

		if (hasGlasses)
		{
			interactTextAlpha = 0f;
		}
	}

	private void ResetView()
	{
		bool isViewChanged = newView != null;

		resetViewTextAlpha = isViewChanged ? 1f : 0f;

		if (isViewChanged)
		{
			interactTextAlpha = 0f;
		}
		
	}
}
