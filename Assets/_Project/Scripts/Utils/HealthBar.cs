using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image image;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        image.fillAmount = currentHealth / maxHealth;
    }
}
