using UnityEngine;

public class NightVision : MonoBehaviour
{
    [Header("Night Vision Settings")]
    [SerializeField] private NightGlasses nightGlasses;
    [SerializeField] private Camera nightCamera;

    public void EnableNightVision()
    {
        nightCamera.gameObject.SetActive(true);
    }

    public void DisableNightVision()
    {
        nightCamera.gameObject.SetActive(false);
    }
}
