using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private Image kaijuHealthBar;
    [SerializeField] private TMP_Text runTimer;
    [SerializeField] private TMP_Text kaijuKnowledgeField;
    [SerializeField] private TMP_Text troopDamageField;

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

    public void UpdateKaijuKnowledge(int coinAmount)
    {
        kaijuKnowledgeField.text = coinAmount.ToString();
    }

    public void UpdateTroopDamage(int coinAmount)
    {
        troopDamageField.text = coinAmount.ToString();
    }

    private void Update()
    {
        
    }
}
