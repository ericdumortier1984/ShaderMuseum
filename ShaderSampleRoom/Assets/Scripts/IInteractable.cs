using UnityEngine;

public interface IInteractable
{
	void Interact();
}

public interface IReleasable
{
	void DropGlasses();
}

public interface IResettable
{
	void ResetPassRendererFeature();
}

public interface IColorable
{
	void SetEdgeColor(Color color);
	void SetInnerColor(Color color);
	Color GetEdgeColor();
	Color GetInnerColor();
}

public interface IPowerable
{
	void SetEdgePower(float value);
	void SetEmissionPower(float value);
	void SetEnergySpeed(float value);
	float GetEdgePower();
	float GetEmissionPower();
}

public interface IFillable
{
	void SetFillVolume(float value);
	float GetFillVolume();
}
