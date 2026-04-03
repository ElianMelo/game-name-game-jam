using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform sphereCastOrigin;
    [SerializeField] private float attackActiveDuration;
    [SerializeField] private InputActionReference attack;
    [SerializeField] public LayerMask layerMask;

    private float currentAttackCooldown;
    private bool canAttack;

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
            currentAttackCooldown = KaijuUpgradeManager.Instance.AttackSpeed;
            canAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (KaijuUpgradeManager.Instance == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sphereCastOrigin.position, KaijuUpgradeManager.Instance.Range);
    }

    private void AttemptAttack(InputAction.CallbackContext context)
    {
        if (!canAttack) return;

        Collider[] hits = Physics.OverlapSphere(
            sphereCastOrigin.position,
            KaijuUpgradeManager.Instance.Range,
            layerMask
        );

        foreach (var hit in hits)
        {
            RegisterTriggerContact(hit);
        }
    }

    public void RegisterTriggerContact(Collider other)
    {
        EnemyController enemyController = other.GetComponent<EnemyController>();
        enemyController.ReceiveDamage(KaijuUpgradeManager.Instance.Damage);
    }
}
