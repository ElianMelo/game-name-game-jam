using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float currentHealth;

    void Start()
    {
        currentHealth = KaijuUpgradeManager.Instance.maxHealth;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameState.Upgrade) return;
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            ReceiveDamage();
        }
        InterfaceManager.Instance.UpdateKaijuHealth(currentHealth, KaijuUpgradeManager.Instance.maxHealth);
    }

    private void ReceiveDamage()
    {
        currentHealth -= 10f;
    }
}
