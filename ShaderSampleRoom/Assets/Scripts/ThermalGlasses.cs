using UnityEngine;
using System.Collections;

public class ThermalGlasses : MonoBehaviour, IInteractable, IDropeable
{
    [SerializeField] private ThermalVision thermalVision;

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
			thermalVision.EnableThermalVision();
			gameObject.SetActive(false);
		}
    }

	public void DropGlasses()
	{
		isInteracted = false;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		thermalVision.DisableThermalVision();
		gameObject.SetActive(true);
	}
}
