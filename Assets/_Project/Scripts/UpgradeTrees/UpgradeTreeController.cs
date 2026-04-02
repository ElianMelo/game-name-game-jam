using UnityEngine;

public class UpgradeTreeController : MonoBehaviour
{
    public GameObject visuals;

    public void ShowVisuals()
    {
        visuals.SetActive(true);
    }

    public void HideVisuals()
    {
        visuals.SetActive(false);
    }
}
