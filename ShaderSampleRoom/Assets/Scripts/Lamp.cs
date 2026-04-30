using UnityEngine;
using System.Collections;

public class Lamp : MonoBehaviour, IInteractable
{
	[Header("Lamp")]
	[SerializeField] private Material lampMaterial;
	[SerializeField] private GameObject uiLampPanel;
	[SerializeField] private ColorController edgeColorController;
	[SerializeField] private ColorController innerColorController;
	[SerializeField] private ValueController edgeController;
	[SerializeField] private ValueController emissionController;
	[SerializeField] private GameObject energySlider;
	[SerializeField] private bool isEnergySphere;

	[Header("On Interaction Settings")]
	[SerializeField] private Transform lookTarget;
	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerInteraction playerInteraction;

	private void Start()
	{
		edgeColorController.SetInitialColor(Color.yellow);
		innerColorController.SetInitialColor(Color.magenta);
	}

	IEnumerator SmoothLook(Quaternion targetRotation)
	{
		Transform cam = player.GetComponentInChildren<Camera>().transform;

		float time = 0f;
		float duration = 0.3f;

		Quaternion startRot = cam.rotation;

		while (time < duration)
		{
			cam.rotation = Quaternion.Slerp(startRot, targetRotation, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		cam.rotation = targetRotation;
	}

	private void LookAtObject()
	{
		Transform cam = player.GetComponentInChildren<Camera>().transform;

		Vector3 direction = lookTarget.position - cam.position;
		Quaternion targetRotation = Quaternion.LookRotation(direction);

		StartCoroutine(SmoothLook(targetRotation));
	}

	public void Interact()
	{
		uiLampPanel.SetActive(true);
		player.EnableControl(false);
		LookAtObject();

		edgeColorController.SetLamp(this);
		innerColorController.SetLamp(this);
		edgeController.SetLamp(this);
		emissionController.SetLamp(this);

		edgeColorController.SetInitialColor(GetEdgeColor());
		innerColorController.SetInitialColor(GetInnerColor());
		edgeController.SetInitialValue(GetEdgePower());
		emissionController.SetInitialValue(GetEmissionPower());


		if (energySlider != null)
		{
			energySlider.SetActive(isEnergySphere);
		}
	}

	public void SetEdgePower(float value)
	{
		lampMaterial.SetFloat("_EdgePower", value);
	}

	public void SetEmissionPower(float value)
	{
		lampMaterial.SetFloat("_EmissionPower", value);
	}

	public void SetEdgeColor(Color color)
	{
		lampMaterial.SetColor("_EdgeColor", color);
	}

	public void SetInnerColor(Color color)
	{
		lampMaterial.SetColor("_InnerColor", color);
	}

	public Color GetEdgeColor()
	{
		return lampMaterial.GetColor("_EdgeColor");
	}

	public Color GetInnerColor()
	{
		return lampMaterial.GetColor("_InnerColor");
	}

	public float GetEdgePower()
	{
		return lampMaterial.GetFloat("_EdgePower");
	}

	public float GetEmissionPower()
	{
		return lampMaterial.GetFloat("_EmissionPower");
	}

	public void SetEnergySpeed(float value)
	{
		lampMaterial.SetFloat("_EnergySpeed", value);
	}

	public void CloseUI()
	{
		uiLampPanel.SetActive(false);
		player.EnableControl(true);
		playerInteraction.SetInteracting(false);
	}

}
