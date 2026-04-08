using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float attackSpeed;
    public Transform sphereCastOrigin;
    public LayerMask layerMask;
    public GameObject projectilePrefab;

    private EnemyController enemyController;
    private Coroutine checkAttackCoroutine;
    private Animator animator;
    private float checkDistance = 0f;

    private const string AttackAnim = "Attack";

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
        checkAttackCoroutine = StartCoroutine(CheckAttack());
        if(enemyController.enemyType == EnemyType.Moving)
        {
            checkDistance = 4f;
        } else  if(enemyController.enemyType == EnemyType.Stationary)
        {
            checkDistance = 100f;
        }
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
        if (Vector3.Distance(transform.position, enemyController.PlayerController.transform.position) > checkDistance) return;
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

        enemyController.OnAttack?.Invoke();

        foreach (var hit in hits)
        {
            RegisterTriggerContact(hit);
        }
    }

    public void PerformProjectileLaunch()
    {
        var direction = enemyController.PlayerController.transform.position - transform.position;
        var projectile = Instantiate(projectilePrefab, sphereCastOrigin.position, Quaternion.LookRotation(direction));
        var enemyProjectile = projectile.GetComponent<EnemyProjectile>();
        enemyProjectile.SetupData(direction, enemyController.GetTroup().damage);
    }

    public void RegisterTriggerContact(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController == null) return;
        playerController.ReceiveDamage(enemyController.GetTroup().damage);
    }
}
