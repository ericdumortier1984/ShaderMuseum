using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
	[SerializeField] private Slider slider;

	private Lamp currentLamp;

	public enum ValueType { EdgePower, Emission }
	public ValueType type;

	public void SetLamp(Lamp lamp)
	{
		currentLamp = lamp;
	}

	public void SetInitialValue(float value)
	{
		slider.value = value;
		UpdateValue();
	}

	public void UpdateValue()
	{
		if (currentLamp == null) return;

		float value = slider.value;

		if (type == ValueType.EdgePower)
		{
			currentLamp.SetEdgePower(value);
		}
		else
		{
			currentLamp.SetEmissionPower(value);
		}
	}
}
