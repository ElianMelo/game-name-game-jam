using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform sphereCastOrigin;
    [SerializeField] private InputActionReference attack;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public AnimationClip attackClip;

    private Animator animator;
    private PlayerController playerController;
    private float currentAttackCooldown;
    private bool canAttack = false;
    private bool isAttackLeft = true;

    private const string AttackLeftAnim = "AttackLeft";
    private const string AttackRightAnim = "AttackRight";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentAttackCooldown = 1f;
    }

    private void OnEnable()
    {
        attack.action.performed += AttemptAttack;
        attack.action.Enable();
    }

    private void OnDisable()
    {
        attack.action.performed -= AttemptAttack;
        attack.action.Disable();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        if (canAttack) return;
        currentAttackCooldown -= Time.deltaTime;
        if(currentAttackCooldown <= 0)
        {
            currentAttackCooldown = attackClip.length / KaijuUpgradeManager.Instance.AttackGroup.attackSpeed;
            canAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (KaijuUpgradeManager.Instance == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sphereCastOrigin.position, KaijuUpgradeManager.Instance.AttackGroup.range);
    }

    private void AttemptAttack(InputAction.CallbackContext context)
    {
        if (playerController.CurrentState == PlayerState.Attacking || playerController.CurrentState == PlayerState.UsingSkill) return;
        if (!canAttack) return;
        canAttack = false;
        isAttackLeft = !isAttackLeft;
        animator.SetTrigger(isAttackLeft ? AttackLeftAnim : AttackRightAnim);
        animator.speed = KaijuUpgradeManager.Instance.AttackGroup.attackSpeed;
        playerController.ChangeState(PlayerState.Attacking);
    }

    public void PerformHitDamage()
    {
        //Collider[] hits = Physics.OverlapSphere(
        //    sphereCastOrigin.position,
        //    KaijuUpgradeManager.Instance.AttackGroup.range,
        //    layerMask
        //);

        playerController.OnAttack?.Invoke();
        playerController.PlayerVFXController.CreateAttackVFX(sphereCastOrigin.position,
            sphereCastOrigin.forward, KaijuUpgradeManager.Instance.AttackGroup.range, KaijuUpgradeManager.Instance.AttackGroup.damage, VFXList.Slash);

        //foreach (var hit in hits)
        //{
        //    RegisterTriggerContact(hit);
        //}
    }

    public void RegisterTriggerContact(Collider other)
    {
        EnemyController enemyController = other.GetComponent<EnemyController>();
        if (enemyController == null) return;
        enemyController.ReceiveDamage(KaijuUpgradeManager.Instance.AttackGroup.damage);
    }
}
