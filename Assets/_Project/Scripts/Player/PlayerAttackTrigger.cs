using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public PlayerAttackController PlayerAttackController {  get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            PlayerAttackController.RegisterTriggerContact(other);
        }
    }
}
