using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
	[SerializeField] private Slider slider;

	private IPowerable currentPowerable;
	private IFillable currentFillable;

	public enum ValueType {
		EdgePower, 
		Emission, 
		Volume
	}

	public ValueType type;

	public void SetObject(IPowerable powerable)
	{
		currentPowerable = powerable;
	}

	public void SetFillableObject(IFillable fillable)
	{
		currentFillable = fillable;
	}

	public void SetInitialValue(float value)
	{
		slider.value = value;
		UpdateValue();
	}

	public void SetFillInitialValue(float value)
	{
		slider.value = value;
		UpdateFillVolume();
	}

	public void UpdateValue()
	{
		if (currentPowerable == null) { return; }

		float value = slider.value;

		if (type == ValueType.EdgePower)
		{
			currentPowerable.SetEdgePower(value);
		}
		else
		{
			currentPowerable.SetEmissionPower(value);
		}
	}

	public void UpdateFillVolume()
	{
		if (currentFillable == null) { return; }
		currentFillable.SetFillVolume(slider.value);
	}
}
