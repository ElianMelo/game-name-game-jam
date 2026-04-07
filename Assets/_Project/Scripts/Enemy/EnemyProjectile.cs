using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;

    public void SetupDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void Start()
    {
        rb.AddForce(direction * 20f, ForceMode.Impulse);
    }
}
