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
        playerController.ResetAnimator();
        playerController.ChangeState(PlayerState.Idling);
    }

    public void PerformAttackHit()
    {
        playerController.PlayerAttackController.PerformHitDamage();
    }

    public void PerformSkillBurstHit()
    {
        playerController.PlayerSkillController.PerformBurstSkillHitDamage();
    }

    public void PerformSkillAreaHit()
    {
        playerController.PlayerSkillController.PerformAreaSkillHitDamage();
    }

    public void PerformSkillProjectileHit()
    {
        playerController.PlayerSkillController.PerformProjectileSkillHitDamage();
    }
}
