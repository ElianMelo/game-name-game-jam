using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health;

    private float currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = health;
    }

    public void ReceiveDamage(float amount)
    {
        if (isDead) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    private void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
