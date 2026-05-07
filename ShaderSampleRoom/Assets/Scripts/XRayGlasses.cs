using UnityEngine;
using System.Collections;

public class XRayGlasses : MonoBehaviour, IInteractable, IDropeable
{
	[SerializeField] private XRayVision xRayVision;

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private bool isInteracted = false;

	private void Start()
	{
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	public void Interact()
	{
		if (!isInteracted)
		{
			xRayVision.EnableXRayVision();
			gameObject.SetActive(false);
		}
	}

	public void DropGlasses()
	{
		isInteracted = false;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		xRayVision.DisableXRayVision();
		gameObject.SetActive(true);
	}
}
