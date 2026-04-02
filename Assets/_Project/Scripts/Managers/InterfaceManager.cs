using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private Image kaijuHealthBar;
    [SerializeField] private TMP_Text runTimer;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateKaijuHealth(float currentHealth, float maxHealth)
    {
        kaijuHealthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateTimer(float currentTimer)
    {
        runTimer.text = currentTimer.ToString("F0");
    }

    private void Update()
    {
        
    }
}
