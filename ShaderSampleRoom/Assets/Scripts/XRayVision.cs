using UnityEngine;

public class XRayVision : MonoBehaviour
{
    [Header("X - Ray Glasses")]
    [SerializeField] private GameObject xRayGlasses;
	[SerializeField] private Camera xRayCamera;

	public void EnableXRayVision()
	{
		xRayCamera.gameObject.SetActive(true);
	}

	public void DisableXRayVision()
	{
		xRayCamera.gameObject.SetActive(false);
	}
}
