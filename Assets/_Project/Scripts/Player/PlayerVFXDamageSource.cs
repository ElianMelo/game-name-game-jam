using UnityEngine;

public class PlayerVFXDamageSource : MonoBehaviour
{
    public float damage;
    public void SetupDamage(float newDamage)
    {
        damage = newDamage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().ReceiveDamage(damage);
        }
    }
}
