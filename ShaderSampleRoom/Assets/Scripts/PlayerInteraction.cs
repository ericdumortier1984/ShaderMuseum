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
