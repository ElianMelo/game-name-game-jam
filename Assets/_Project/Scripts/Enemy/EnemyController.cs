using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = health;
        healthBar.UpdateHealth(currentHealth, health);
    }

    public void ReceiveDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.UpdateHealth(currentHealth, health);
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
