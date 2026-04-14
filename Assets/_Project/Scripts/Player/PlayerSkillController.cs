using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private Transform skillBurstOrigin;
    [SerializeField] private Transform skillAreaOrigin;
    [SerializeField] private Transform skillProjectileOrigin;
    [SerializeField] private InputActionReference skillBurst;
    [SerializeField] private InputActionReference skillArea;
    [SerializeField] private InputActionReference skillProjectile;
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public AnimationClip burstClip;
    [SerializeField] public AnimationClip areaClip;
    [SerializeField] public AnimationClip projectileClip;
    [SerializeField] public GameObject rollSkillVFX;

    private Animator animator;
    private PlayerController playerController;

    private bool canUseSkillBurst = true;
    private bool canUseSkillArea = true;
    private bool canUseSkillProjectile = true;

    private const string SkillBurstAnim = "SkillBurst";
    private const string SkillAreaAnim = "SkillArea";
    private const string SkillProjectileAnim = "SkillRoll";

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        skillBurst.action.performed += AttemptSkillBurst;
        skillArea.action.performed += AttemptSkillArea;
        skillProjectile.action.performed += AttemptSkillProjectile;
        skillBurst.action.Enable();
        skillArea.action.Enable();
        skillProjectile.action.Enable();
    }

    private void OnDisable()
    {
        skillBurst.action.performed -= AttemptSkillBurst;
        skillArea.action.performed -= AttemptSkillArea;
        skillProjectile.action.performed -= AttemptSkillProjectile;
        skillBurst.action.Disable();
        skillArea.action.Disable();
        skillProjectile.action.Disable();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
    }

    private void OnDrawGizmos()
    {
        if (KaijuUpgradeManager.Instance == null) return;
        Gizmos.color = Color.green;
    }

    private void AttemptSkillBurst(InputAction.CallbackContext context)
    {
        if (playerController.CurrentState == PlayerState.Attacking || playerController.CurrentState == PlayerState.UsingSkill) return;
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillBurst.unlocked) return;
        if (!canUseSkillBurst) return;
        SoundManager.Instance.PlayerBurstSkill();
        playerController.OnSkill?.Invoke();
        canUseSkillBurst = false;
        animator.SetTrigger(SkillBurstAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillBurst.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetBurstSkill());
        IEnumerator ResetBurstSkill()
        {
            yield return new WaitForSeconds(burstClip.length / KaijuUpgradeManager.Instance.SkillBurst.skillCooldown);
            canUseSkillBurst = true;
        }
    }

    private void AttemptSkillArea(InputAction.CallbackContext context)
    {
        if (playerController.CurrentState == PlayerState.Attacking || playerController.CurrentState == PlayerState.UsingSkill) return;
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillArea.unlocked) return;
        if (!canUseSkillArea) return;
        SoundManager.Instance.PlayerAreaSkill();
        playerController.OnSkill?.Invoke();
        canUseSkillArea = false;
        animator.SetTrigger(SkillAreaAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillArea.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetAreaSkill());
        IEnumerator ResetAreaSkill()
        {
            yield return new WaitForSeconds(areaClip.length / KaijuUpgradeManager.Instance.SkillArea.skillCooldown);
            canUseSkillArea = true;
        }
    }


    private void AttemptSkillProjectile(InputAction.CallbackContext context)
    {
        if (playerController.CurrentState == PlayerState.Attacking || playerController.CurrentState == PlayerState.UsingSkill) return;
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillProjectile.unlocked) return;
        if (!canUseSkillProjectile) return;
        SoundManager.Instance.PlayerRollSkill();
        playerController.OnSkill?.Invoke();
        canUseSkillProjectile = false;
        animator.SetTrigger(SkillProjectileAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillProjectile.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetProjectileSkill());
        IEnumerator ResetProjectileSkill()
        {
            yield return new WaitForSeconds(projectileClip.length / KaijuUpgradeManager.Instance.SkillProjectile.skillCooldown);
            canUseSkillProjectile = true;
        }
    }

    public void PerformBurstSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateAttackVFX(skillBurstOrigin.position,
            skillBurstOrigin.forward, KaijuUpgradeManager.Instance.SkillBurst.range, KaijuUpgradeManager.Instance.SkillBurst.damage, VFXList.Burst);
    }

    public void PerformAreaSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateAttackVFX(skillAreaOrigin.position,
            skillAreaOrigin.forward, KaijuUpgradeManager.Instance.SkillArea.range, KaijuUpgradeManager.Instance.SkillArea.damage, VFXList.Area);
    }

    public void PerformProjectileSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateAttackVFX(skillProjectileOrigin.position,
            skillProjectileOrigin.forward, KaijuUpgradeManager.Instance.SkillProjectile.range, KaijuUpgradeManager.Instance.SkillProjectile.damage, VFXList.Projectile);
    }

    public void StartRollSkill()
    {
        rollSkillVFX.SetActive(true);
    }

    public void StopRollSkill()
    {
        rollSkillVFX.SetActive(false);
    }
}
