using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform sphereCastOrigin;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackActiveDuration;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private float damage;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private float distance = 5f;
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
        if (GameManager.Instance.CurrentState == GameState.Upgrade) return;
        if (canAttack) return;
        currentAttackCooldown -= Time.deltaTime;
        if(currentAttackCooldown <= 0)
        {
            currentAttackCooldown = 0;
            canAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sphereCastOrigin.position, radius);
    }

    private void AttemptAttack(InputAction.CallbackContext context)
    {
        if (!canAttack) return;

        Collider[] hits = Physics.OverlapSphere(
            sphereCastOrigin.position,
            radius,
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
        enemyController.ReceiveDamage(damage);
    }
}
