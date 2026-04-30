using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
	[Header("Color Sliders")]
	[SerializeField] private Slider redSlider;
	[SerializeField] private Slider greenSlider;
	[SerializeField] private Slider blueSlider;

	[Header("Game Object")]
	[SerializeField] private Lamp lamp;

	[Header("Preview")]
	[SerializeField] private Image preview;

	private Lamp currentLamp;

	public enum ColorType { Edge, Inner }
	public ColorType type;

	public void SetLamp(Lamp lamp) 
	{
		currentLamp = lamp;
	}

	public void SetInitialColor(Color color)
	{
		redSlider.value = color.r;
		greenSlider.value = color.g;
		blueSlider.value = color.b;

		UpdateColor();
	}

	public void UpdateColor()
	{
		if (currentLamp == null) { return; }

		Color color = new Color(redSlider.value, greenSlider.value, blueSlider.value);

		preview.color = color;

		if (type == ColorType.Edge)
		{
			currentLamp.SetEdgeColor(color);
		}
		else
		{
			currentLamp.SetInnerColor(color);
		}
	}
}
