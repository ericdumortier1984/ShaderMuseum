using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
	[Header("Color Sliders")]
	[SerializeField] private Slider redSlider;
	[SerializeField] private Slider greenSlider;
	[SerializeField] private Slider blueSlider;

	[Header("Preview")]
	[SerializeField] private Image preview;

	private IColorable currentColorable;

	public enum ColorType {
		Edge, 
		Inner
	}

	public ColorType type;

	public void SetObject(IColorable colorable)
	{
		currentColorable = colorable;
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
		if (currentColorable == null) { return; }

		Color color = new Color(
			redSlider.value, 
			greenSlider.value, 
			blueSlider.value
		);

		preview.color = color;

		if (type == ColorType.Edge)
		{
			currentColorable.SetEdgeColor(color);
		}
		else
		{
			currentColorable.SetInnerColor(color);
		}
	}
}
