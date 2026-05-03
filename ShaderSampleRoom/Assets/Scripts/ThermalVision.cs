using UnityEngine;

public class ThermalVision : MonoBehaviour
{

	[Header("Thermal Vision Settings")]
	[SerializeField] private GameObject thermalGlasses;
	[SerializeField] private Camera thermalCamera;

	public void EnableThermalVision()
	{
		thermalCamera.gameObject.SetActive(true);
	}

	public void DisableThermalVision()
	{
		thermalCamera.gameObject.SetActive(false);
	}
}
