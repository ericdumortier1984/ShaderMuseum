using UnityEngine;

public class XRayVision : MonoBehaviour
{
    [Header("X - Ray Glasses")]
    [SerializeField] private GameObject xRayGlasses;
	[SerializeField] private Camera xRayCamera;

	[Header("On Interaction Settings")]
	//[SerializeField] private Transform lookTarget;
	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerInteraction playerInteraction;

	public void EnableXRayVision()
	{
		xRayCamera.enabled = true;
		Debug.Log(xRayCamera.cullingMask);
	}
}
