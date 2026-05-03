using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
	[Header("Interaction Settings")]
	[SerializeField] private CanvasGroup interactCanvas;
	[SerializeField] private float fadeSpeed = 5f;
	[SerializeField] private float distance = 3f;
	[SerializeField] private Camera cam;

	PlayerInputActions input;

	private bool isInteracting = false;
	private float targetAlpha = 0f;

	private XRayGlasses xRayGlasses;
	private ThermalGlasses thermalGlasses;
	private NightGlasses nightGlasses;

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
		if (xRayGlasses != null)
		{
			xRayGlasses.DropGlasses();
			xRayGlasses = null;
			isInteracting = false;
			return;
		}

		if (thermalGlasses != null)
		{
			thermalGlasses.DropGlasses();
			thermalGlasses = null;
			isInteracting = false;
			return;
		}

		if (nightGlasses != null)
		{
			nightGlasses.DropGlasses();
			nightGlasses = null;
			isInteracting = false;
			return;
		}

		Interact();
	}

	private void Interact()
	{
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);

		if (Physics.Raycast(ray, out RaycastHit hit, distance))
		{
			var interactable = hit.collider.GetComponent<IInteractable>();

			if (interactable != null)
			{
				interactable.Interact();

				var currentXRayGlasses = hit.collider.GetComponent<XRayGlasses>();
				if (currentXRayGlasses != null)
				{
					xRayGlasses = currentXRayGlasses;
				}

				var currentThermalGlasses = hit.collider.GetComponent<ThermalGlasses>();
				if (currentThermalGlasses != null)
				{
					thermalGlasses = currentThermalGlasses;
				}

				var currentNightGlasses = hit.collider.GetComponent<NightGlasses>();
				if (currentNightGlasses != null)
				{
					nightGlasses = currentNightGlasses;
				}

				SetInteracting(true);
			}
		}
	}

	private void CheckInteractable()
	{
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);

		bool hasInteractable = false;

		if (Physics.Raycast(ray, out RaycastHit hit, distance))
		{
			hasInteractable = hit.collider.GetComponent<IInteractable>() != null;
		}

		targetAlpha = hasInteractable ? 1f : 0f;
	}

	public void SetInteracting(bool value)
	{
		isInteracting = value;
		targetAlpha = 0f;

		if (value)
		{
			interactCanvas.alpha = 0f; 
		}
	}

	private void FadeOut()
	{
		interactCanvas.alpha = Mathf.Lerp(interactCanvas.alpha,targetAlpha,Time.deltaTime * fadeSpeed);
	}
}
