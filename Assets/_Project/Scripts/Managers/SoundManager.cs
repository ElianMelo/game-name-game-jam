using MoreMountains.Feedbacks;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private MMFeedbacks music;

    [Header("SFX")]
    [SerializeField] private MMFeedbacks movingEnemyHit;
    [SerializeField] private MMFeedbacks movingEnemyDeath;
    [SerializeField] private MMFeedbacks stationaryEnemyHit;
    [SerializeField] private MMFeedbacks stationaryEnemyDeath;

    [SerializeField] private MMFeedbacks playerClawAttack;
    [SerializeField] private MMFeedbacks playerBurstSkill;
    [SerializeField] private MMFeedbacks playerAreaSkill;
    [SerializeField] private MMFeedbacks playerRollSkill;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        music?.PlayFeedbacks();
    }

    public void MovingEnemyHit() { movingEnemyHit?.PlayFeedbacks(); }
    public void MovingEnemyDeath() { movingEnemyDeath?.PlayFeedbacks(); }
    public void StationaryEnemyHit() { stationaryEnemyHit?.PlayFeedbacks(); }
    public void StationaryEnemyDeath() { stationaryEnemyDeath?.PlayFeedbacks(); }
    public void PlayerClawAttack() { playerClawAttack?.PlayFeedbacks(); }
    public void PlayerBurstSkill() { playerBurstSkill?.PlayFeedbacks(); }
    public void PlayerAreaSkill() { playerAreaSkill?.PlayFeedbacks(); }
    public void PlayerRollSkill() { playerRollSkill?.PlayFeedbacks(); }
}
