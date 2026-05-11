using UnityEngine;
using System.Collections;

public class FullScreenButton : MonoBehaviour, IInteractable, IResettable
{
	[Header("Full Screen Button Settings")]
	[SerializeField] private UnityEngine.Rendering.Universal.FullScreenPassRendererFeature fullScreenPassRendererFeature;

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
