using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0f;
    [SerializeField] private Slider xRotationSlider;
    [SerializeField] private Slider zRotationSlider;

    private Transform targetObject;

    public void SetBottle(Transform currentBottle)
    {
        targetObject = currentBottle;
    }

    public void UpdateRotation()
    {
        float xRotation = xRotationSlider.value * rotationSpeed;
        float zRotation = zRotationSlider.value * rotationSpeed;
        targetObject.rotation = Quaternion.Euler(xRotation, 0f, zRotation);
    }
}
