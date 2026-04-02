using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public PlayerAttackController PlayerAttackController {  get; set; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if(other.CompareTag("Enemy"))
        {
            PlayerAttackController.RegisterTriggerContact(other);
        }
    }
}
