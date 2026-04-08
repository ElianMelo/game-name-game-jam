using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    [HideInInspector] public float damage;

    public void SetupData(Vector3 newDirection, float newDamage)
    {
        direction = newDirection;
        damage = newDamage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * 20f, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }
}
