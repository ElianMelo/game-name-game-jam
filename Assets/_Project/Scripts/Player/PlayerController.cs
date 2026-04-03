using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float currentHealth;

    void Start()
    {
        currentHealth = KaijuUpgradeManager.Instance.MaxHealth;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ReceiveDamage();
        }
        InterfaceManager.Instance.UpdateKaijuHealth(currentHealth, KaijuUpgradeManager.Instance.MaxHealth);
    }

    private void ReceiveDamage()
    {
        currentHealth -= 10f;
    }
}
