using UnityEngine;

public class PlayerVFXDamageSource : MonoBehaviour
{
    public float damage;
    private Collider vfxCollider;
    public void SetupDamage(float newDamage)
    {
        damage = newDamage;
        vfxCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().ReceiveDamage(damage);
            Physics.IgnoreCollision(vfxCollider, other);
        }
    }
}
