using UnityEngine;

public class EnemyAnimatorEvents : MonoBehaviour
{
    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    public void FinishAttackState()
    {
        enemyController.ResetAnimator();
        enemyController.ChangeState(EnemyState.Moving);
    }

    public void PerformAttackHit()
    {
        enemyController.EnemyAttackController.PerformHitDamage();
    }

    public void PerformProjectileLaunch()
    {
        enemyController.EnemyAttackController.PerformProjectileLaunch();
    }
}
