using FIMSpace.Basics;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum EnemyType
{
    Moving,
    Stationary
}

public enum EnemyState
{
    Attacking,
    Moving,
}

public class EnemyController : MonoBehaviour
{
    public float health;
    public EnemyType enemyType;
    public TroopName troopName;
    private HealthBar healthBar;
    private Animator animator;

    public EnemyMovementController EnemyMovementController { get; private set; }
    public EnemyAttackController EnemyAttackController { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public EnemyState CurrentState { get; private set; }

    private float currentHealth;
    private bool isDead = false;

    public UnityEvent OnHurt;
    public UnityEvent OnDead;
    public UnityEvent OnMoveStart;
    public UnityEvent OnMoveStop;
    public UnityEvent OnAttack;

    private void Start()
    {
        currentHealth = GetTroup().health;
        health = currentHealth;
        animator = GetComponentInChildren<Animator>();
        EnemyMovementController = GetComponent<EnemyMovementController>();
        EnemyAttackController = GetComponent<EnemyAttackController>();
        healthBar = GetComponentInChildren<HealthBar>();
        PlayerController = FindFirstObjectByType<PlayerController>();
        healthBar.UpdateHealth(currentHealth, health);
        ChangeState(EnemyState.Moving);
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if (enemyType == EnemyType.Stationary)
            transform.forward = PlayerController.transform.position - transform.position;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    public TroopAttributesGroup GetTroup()
    {
        return TroopUpgradeManager.Instance.GetTroopByName(troopName);
    }

    private void OnGameStateChanged(GameState gamestate)
    {
        if (gamestate == GameState.Upgrade) Death();
    }

    public void ResetAnimator()
    {
        animator.speed = 1f;
    }

    public void ChangeState(EnemyState enemyState)
    {
        if(enemyState == EnemyState.Moving)
        {
            OnMoveStart?.Invoke();
        } else
        {
            OnMoveStop?.Invoke();
        }
        CurrentState = enemyState;
    }

    public void ReceiveDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        OnHurt?.Invoke();
        StartCoroutine(Knockback(transform.position - PlayerController.transform.position, 3f, 0.1f));
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth, health);
            GameManager.Instance.AddKaijuuKnowledge(5);
            OnDead?.Invoke();
            TriggerDeathParticle();
            Death();
        } else
        {
            healthBar.UpdateHealth(currentHealth, health);
        }
    }

    public IEnumerator Knockback(Vector3 direction, float force, float duration)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.isStopped = true;
        agent.updatePosition = false;

        float timer = 0f;

        while (timer < duration)
        {
            transform.position += direction * force * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        agent.updatePosition = true;
        agent.Warp(transform.position); // sync agent
        agent.isStopped = false;
    }

    private void TriggerDeathParticle()
    {
        GameObject enemyVFX = enemyType == EnemyType.Moving ? PoolsManager.Instance.GetEnemyVFX(2) : PoolsManager.Instance.GetEnemyStationaryVFX(2);
        enemyVFX.SetActive(true);
        enemyVFX.transform.position = transform.position;
        enemyVFX.transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
        enemyVFX.GetComponent<ParticleSystem>().Play();
    }

    private void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
