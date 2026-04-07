using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float attackSpeed;
    public Transform sphereCastOrigin;
    public LayerMask layerMask;

    private EnemyController enemyController;
    private Coroutine checkAttackCoroutine;
    private Animator animator;

    private const string AttackAnim = "Attack";

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
        checkAttackCoroutine = StartCoroutine(CheckAttack());
    }

    private IEnumerator CheckAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / enemyController.GetTroup().attackSpeed);
            AttemptAttack();
        }
    }

    private void AttemptAttack()
    {
        if (Vector3.Distance(transform.position, enemyController.PlayerController.transform.position) > 3f) return;
        animator.SetTrigger(AttackAnim);
        animator.speed = enemyController.GetTroup().attackSpeed;
        enemyController.ChangeState(EnemyState.Attacking);
    }

    public void PerformHitDamage()
    {
         Collider[] hits = Physics.OverlapSphere(
            sphereCastOrigin.position,
            enemyController.GetTroup().range,
            layerMask
        );

        foreach (var hit in hits)
        {
            RegisterTriggerContact(hit);
        }
    }

    public void RegisterTriggerContact(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        playerController.ReceiveDamage(enemyController.GetTroup().damage);
    }
}
