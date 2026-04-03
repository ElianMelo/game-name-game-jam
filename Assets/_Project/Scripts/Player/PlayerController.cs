using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState { 
    Attacking,
    Moving,
    Idling,
    UsingSkill
}

public class PlayerController : MonoBehaviour
{
    private float currentHealth;
    public PlayerState CurrentState { get; private set; }

    void Start()
    {
        CurrentState = PlayerState.Idling;
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

    public void ChangeState(PlayerState playerState)
    {
        CurrentState = playerState;
    }

    private void ReceiveDamage()
    {
        currentHealth -= 10f;
    }
}
