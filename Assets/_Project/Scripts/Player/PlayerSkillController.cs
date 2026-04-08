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

    private Animator animator;
    private PlayerController playerController;

    private bool canUseSkillBurst = true;
    private bool canUseSkillArea = true;
    private bool canUseSkillProjectile = true;

    private const string SkillBurstAnim = "SkillBurst";
    private const string SkillAreaAnim = "SkillArea";
    private const string SkillProjectileAnim = "SkillProjectile";

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
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillBurst.unlocked) return;
        if (!canUseSkillBurst) return;
        playerController.OnSkill?.Invoke();
        canUseSkillBurst = false;
        animator.SetTrigger(SkillBurstAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillBurst.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetBurstSkill());
        IEnumerator ResetBurstSkill()
        {
            yield return new WaitForSeconds(1 / KaijuUpgradeManager.Instance.SkillBurst.skillCooldown);
            canUseSkillBurst = true;
        }
    }

    private void AttemptSkillArea(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillArea.unlocked) return;
        if (!canUseSkillArea) return;
        playerController.OnSkill?.Invoke();
        canUseSkillArea = false;
        animator.SetTrigger(SkillAreaAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillArea.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetAreaSkill());
        IEnumerator ResetAreaSkill()
        {
            yield return new WaitForSeconds(1 / KaijuUpgradeManager.Instance.SkillArea.skillCooldown);
            canUseSkillArea = true;
        }
    }


    private void AttemptSkillProjectile(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.currentState != GameState.KaijuControl) return;
        if (!KaijuUpgradeManager.Instance.SkillProjectile.unlocked) return;
        if (!canUseSkillProjectile) return;
        playerController.OnSkill?.Invoke();
        canUseSkillProjectile = false;
        animator.SetTrigger(SkillProjectileAnim);
        animator.speed = KaijuUpgradeManager.Instance.SkillProjectile.skillCooldown;
        playerController.ChangeState(PlayerState.UsingSkill);
        StartCoroutine(ResetProjectileSkill());
        IEnumerator ResetProjectileSkill()
        {
            yield return new WaitForSeconds(1 / KaijuUpgradeManager.Instance.SkillProjectile.skillCooldown);
            canUseSkillProjectile = true;
        }
    }

    public void PerformBurstSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateBurstSkillVFX(skillBurstOrigin.position,
            skillBurstOrigin.forward, KaijuUpgradeManager.Instance.SkillBurst.range, KaijuUpgradeManager.Instance.SkillBurst.damage);
    }

    public void PerformAreaSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateAreaSkillVFX(skillAreaOrigin.position,
            skillAreaOrigin.forward, KaijuUpgradeManager.Instance.SkillArea.range, KaijuUpgradeManager.Instance.SkillArea.damage);
    }

    public void PerformProjectileSkillHitDamage()
    {
        playerController.PlayerVFXController.CreateProjectileSkillVFX(skillProjectileOrigin.position,
            skillProjectileOrigin.forward, KaijuUpgradeManager.Instance.SkillProjectile.range, KaijuUpgradeManager.Instance.SkillProjectile.damage);
    }
}
