using UnityEngine;

public enum EnemyType
{
    Moving,
    Stationary
}

public class EnemyController : MonoBehaviour
{
    public float health;
    public EnemyType enemyType;
    private HealthBar healthBar;

    private float currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealth(currentHealth, health);
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gamestate)
    {
        if (gamestate == GameState.Upgrade) Death();
    }

    public void ReceiveDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth, health);
            GameManager.Instance.AddCoin(5);
            Death();
        } else
        {
            healthBar.UpdateHealth(currentHealth, health);
        }
    }

    private void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
