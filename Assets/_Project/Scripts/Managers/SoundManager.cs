using MoreMountains.Feedbacks;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private MMFeedbacks music;

    [Header("SFX")]
    [Header("Enemy")]
    [SerializeField] private MMFeedbacks movingEnemyHit;
    [SerializeField] private MMFeedbacks movingEnemyDeath;
    [SerializeField] private MMFeedbacks stationaryEnemyHit;
    [SerializeField] private MMFeedbacks stationaryEnemyDeath;

    [Header("Player")]
    [SerializeField] private MMFeedbacks playerClawAttack;
    [SerializeField] private MMFeedbacks playerBurstSkill;
    [SerializeField] private MMFeedbacks playerAreaSkill;
    [SerializeField] private MMFeedbacks playerRollSkill;
    [SerializeField] private MMFeedbacks playerHurt;

    [Header("UI")]
    [SerializeField] private MMFeedbacks uIBuy;
    [SerializeField] private MMFeedbacks uIUnlock;
    [SerializeField] private MMFeedbacks uIHover;
    [SerializeField] private MMFeedbacks uINoFounds;
    [SerializeField] private MMFeedbacks uIStartBattle;
    [SerializeField] private MMFeedbacks uISwitchTab;

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
    public void PlayerHurt() { playerHurt?.PlayFeedbacks(); }
    public void UIBuy() { uIBuy?.PlayFeedbacks(); }
    public void UIUnlock() { uIUnlock?.PlayFeedbacks(); }
    public void UIHover() { uIHover?.PlayFeedbacks(); }
    public void UINoFounds() { uINoFounds?.PlayFeedbacks(); }
    public void UIStartBattle() { uIStartBattle?.PlayFeedbacks(); }
    public void UISwitchTab() { uISwitchTab?.PlayFeedbacks(); }
}
