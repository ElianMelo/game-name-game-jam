using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    private float damage;

    public void SetupData(Vector3 newDirection, float newDamage)
    {
        direction = newDirection;
        damage = newDamage;
    }

    private void Start()
    {
        rb.AddForce(direction.normalized, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ReceiveDamage(damage);
            // Destroy(gameObject);
        }
    }
}
