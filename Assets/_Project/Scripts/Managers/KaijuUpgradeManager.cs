using System;
using UnityEngine;

public enum UpgradeType
{
    Damage,
    Range,
    Speed,
    AttackSpeed,
    Cooldown,
    Health,
    SpawnAmount,
    SpawnSpeed,
    Unlock
}

public enum UpgradeClass { 
    Kaijuu,
    Troop
}

public enum KaijuuAttibuteGroupType { 
    Attack,
    SkillAOE,
    SkillBurst,
    SkillProjectiles
}

[Serializable]
public class KaijuuAttributesGroup
{
    public KaijuuAttibuteGroupType groupType;
    public bool unlocked = false;
    public float damage;
    public float range;
    public float attackSpeed;
    public float skillCooldown;
}

public class KaijuUpgradeManager : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healthRegen;
    [SerializeField] private float _speed;
    [SerializeField] private KaijuuAttributesGroup _attackGroup;
    [SerializeField] private KaijuuAttributesGroup _skillAOE;
    [SerializeField] private KaijuuAttributesGroup _skillBurst;
    [SerializeField] private KaijuuAttributesGroup _skillProjectile;

    public float MaxHealth => _maxHealth;
    public float HealthRegen => _healthRegen;
    public float Speed => _speed;
    public KaijuuAttributesGroup AttackGroup => _attackGroup;
    public KaijuuAttributesGroup SkillArea => _skillAOE;
    public KaijuuAttributesGroup SkillBurst => _skillBurst;
    public KaijuuAttributesGroup SkillProjectile => _skillProjectile;
    public PlayerController Controller { get; private set; }

    public static KaijuUpgradeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Controller = FindAnyObjectByType<PlayerController>();
    }

    public void BuyUpgrade(UpgradeType upgradeType, KaijuuAttibuteGroupType groupType, float amount)
    {
        if(upgradeType == UpgradeType.Unlock)
        {
            switch (groupType)
            {
                case KaijuuAttibuteGroupType.SkillAOE: _skillAOE.unlocked = true; break;
                case KaijuuAttibuteGroupType.SkillBurst: _skillBurst.unlocked = true; break;
                case KaijuuAttibuteGroupType.SkillProjectiles: _skillProjectile.unlocked = true; break;
            }
            UpgradeTreeSwitcher.UnlockKaijuuSkillTreePath?.Invoke(groupType);
        }
        switch (groupType)
        {
            case KaijuuAttibuteGroupType.Attack:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _attackGroup.damage += amount; return;
                    case UpgradeType.Range: _attackGroup.range += amount; return;
                    case UpgradeType.AttackSpeed: _attackGroup.attackSpeed += amount; return;
                    case UpgradeType.Cooldown: _attackGroup.skillCooldown += amount; return;
                    default: return;
                }
            case KaijuuAttibuteGroupType.SkillAOE:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _skillAOE.damage += amount; return;
                    case UpgradeType.Range: _skillAOE.range += amount; return;
                    case UpgradeType.AttackSpeed: _skillAOE.attackSpeed += amount; return;
                    case UpgradeType.Cooldown: _skillAOE.skillCooldown += amount; return;
                    default: return;
                }
            case KaijuuAttibuteGroupType.SkillBurst:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _skillBurst.damage += amount; return;
                    case UpgradeType.Range: _skillBurst.range += amount; return;
                    case UpgradeType.AttackSpeed: _skillBurst.attackSpeed += amount; return;
                    case UpgradeType.Cooldown: _skillBurst.skillCooldown += amount; return;
                    default: return;
                }
            case KaijuuAttibuteGroupType.SkillProjectiles:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _skillProjectile.damage += amount; return;
                    case UpgradeType.Range: _skillProjectile.range += amount; return;
                    case UpgradeType.AttackSpeed: _skillProjectile.attackSpeed += amount; return;
                    case UpgradeType.Cooldown: _skillProjectile.skillCooldown += amount; return;
                    default: return;
                }
            default:
                break;
        }
        switch (upgradeType)    
        {
            case UpgradeType.Speed: _speed += amount; return;
            case UpgradeType.Health: _maxHealth += amount; return;
            default: return;
        }
    }
}

