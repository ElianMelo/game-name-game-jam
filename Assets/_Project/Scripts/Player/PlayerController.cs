using UnityEngine;

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
    private Animator animator;
    public PlayerAttackController PlayerAttackController { get; private set; }
    public PlayerMovementController PlayerMovementController { get; private set; }
    public PlayerVFXController PlayerVFXController { get; private set; }

    void Start()
    {
        PlayerAttackController = GetComponent<PlayerAttackController>();
        PlayerMovementController = GetComponent<PlayerMovementController>();
        PlayerVFXController = GetComponent<PlayerVFXController>();
        animator = GetComponentInChildren<Animator>();
        CurrentState = PlayerState.Idling;
        currentHealth = KaijuUpgradeManager.Instance.MaxHealth;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        InterfaceManager.Instance.UpdateKaijuHealth(currentHealth, KaijuUpgradeManager.Instance.MaxHealth);
    }

    public void ResetAnimator()
    {
        animator.speed = 1f;
    }

    public void ChangeState(PlayerState playerState)
    {
        CurrentState = playerState;
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
    }
}
