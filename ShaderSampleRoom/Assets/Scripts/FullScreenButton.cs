using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class FullScreenButton : MonoBehaviour, IInteractable, IReseteable
{
	[Header("Full Screen Button Settings")]
	[SerializeField] private FullScreenPassRendererFeature fullScreenPassRendererFeature;

	public void Interact()
	{
		TogglePassRendererFeature();
	}

	public void ResetPassRendererFeature()
	{
		fullScreenPassRendererFeature.SetActive(false);
	}

	private void TogglePassRendererFeature()
	{
		fullScreenPassRendererFeature.SetActive(true);
	}
}
