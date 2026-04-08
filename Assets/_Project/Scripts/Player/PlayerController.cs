using UnityEngine;
using UnityEngine.Events;

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
    public PlayerSkillController PlayerSkillController { get; private set; }
    public PlayerMovementController PlayerMovementController { get; private set; }
    public PlayerVFXController PlayerVFXController { get; private set; }

    public UnityEvent OnHurt;
    public UnityEvent OnDead;
    public UnityEvent OnMoveStart;
    public UnityEvent OnMoveStop;
    public UnityEvent OnAttack;
    public UnityEvent OnSkill;


    void Start()
    {
        PlayerAttackController = GetComponent<PlayerAttackController>();
        PlayerSkillController = GetComponent<PlayerSkillController>();
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
        if(playerState == PlayerState.Moving)
        {
            OnMoveStart?.Invoke();
        } else
        {
            OnMoveStop?.Invoke();
        }
        CurrentState = playerState;
    }

    public void ReceiveDamage(float amount)
    {
        currentHealth -= amount;
        OnHurt?.Invoke();
        GameManager.Instance.AddTroopDamage((int)amount);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDead?.Invoke();
        }
    }
}
