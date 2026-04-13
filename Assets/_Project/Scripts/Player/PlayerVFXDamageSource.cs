using System.Collections;
using UnityEngine;

public class PlayerVFXDamageSource : MonoBehaviour
{
    public float damage;
    public bool isRollSKill = false;
    private Collider vfxCollider;
    public void SetupDamage(float newDamage)
    {
        damage = newDamage;
        vfxCollider = GetComponent<Collider>();
        StartCoroutine(DisableCollider());
        IEnumerator DisableCollider()
        {
            yield return new WaitForSeconds(0.1f);
            if(!isRollSKill)
                vfxCollider.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().ReceiveDamage(damage);
            if(!isRollSKill)
                Physics.IgnoreCollision(vfxCollider, other);
        }
    }
}
