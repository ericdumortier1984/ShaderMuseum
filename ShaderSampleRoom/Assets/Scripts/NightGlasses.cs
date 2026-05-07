using UnityEngine;
using System.Collections;

public class NightGlasses : MonoBehaviour, IInteractable, IDropeable
{
    [SerializeField] private NightVision nightVision;

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
			nightVision.EnableNightVision();
			gameObject.SetActive(false);
		}
    }

	public void DropGlasses()
	{
		isInteracted = false;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		nightVision.DisableNightVision();
		gameObject.SetActive(true);
	}
}
