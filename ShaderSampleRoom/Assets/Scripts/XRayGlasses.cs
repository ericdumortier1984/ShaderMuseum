using UnityEngine;
using System.Collections;

public class XRayGlasses : MonoBehaviour, IInteractable
{
	[SerializeField] private XRayVision xRayVision;

	public void Interact()
	{
		Debug.Log("INTERACT WITH W RAY GLASSES");
		xRayVision.EnableXRayVision();
		gameObject.SetActive(false);
	}
}
