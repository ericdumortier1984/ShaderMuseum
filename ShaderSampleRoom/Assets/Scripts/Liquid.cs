using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour, IInteractable, IColorable, IFillable
{
    [Header("Liquid Settings")]
    [SerializeField] private Material liquid;
	[SerializeField] private Transform liquidBottle;
	[SerializeField] private ColorController edgeColorController;
	[SerializeField] private ColorController innerColorController;
	[SerializeField] private ValueController fillController;
	[SerializeField] private RotationController rotationController;

	[Header("On Interaction Settings")]
	[SerializeField] private Transform lookTarget;
	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerInteraction playerInteraction;
	[SerializeField] private GameObject uiLiquidPanel;

	private void Start()
	{
		edgeColorController.SetInitialColor(Color.green);
		innerColorController.SetInitialColor(Color.green);
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
        player.EnableControl(false);
		LookAtObject();
		uiLiquidPanel.SetActive(true);

		edgeColorController.SetObject(this);
		innerColorController.SetObject(this);
		fillController.SetFillableObject(this);
		
		edgeColorController.SetInitialColor(GetEdgeColor());
		innerColorController.SetInitialColor(GetInnerColor());
		fillController.SetFillInitialValue(GetFillVolume());

		rotationController.SetBottle(liquidBottle);
	}


	public void SetEdgeColor(Color color)
	{
		liquid.SetColor("_OutsideColor", color);
	}

	public void SetInnerColor(Color color)
	{
		liquid.SetColor("_InsideColor", color);
	}

	public void SetFillVolume(float value)
	{
		liquid.SetFloat("_FillVolume", value);
	}

	public Color GetEdgeColor()
	{
		return liquid.GetColor("_OutsideColor");
	}

	public Color GetInnerColor()
	{
		return liquid.GetColor("_InsideColor");
	}

	public float GetFillVolume()
	{
		return liquid.GetFloat("_FillVolume");
	}

	public void CloseUI()
	{
		uiLiquidPanel.SetActive(false);
		player.EnableControl(true);
		playerInteraction.SetInteracting(false);
	}
}
