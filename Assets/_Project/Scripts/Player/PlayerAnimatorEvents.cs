using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void FinishAttackState()
    {
        playerController.ChangeState(PlayerState.Idling);
    }
}
