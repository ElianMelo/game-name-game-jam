using UnityEngine;

public enum UpgradeType
{
    Damage,
    Range,
    Speed,
    AttackSpeed,
    Health,
    AmountTroop,
    AmountGroup
}

public enum UpgradeClass { 
    Kaijuu,
    Troop
}

public class KaijuUpgradeManager : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _healthRegen;
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _skillCooldown;

    public float MaxHealth => _maxHealth;
    public float HealthRegen => _healthRegen;
    public float Damage => _damage;
    public float Range => _range;
    public float Speed => _speed;
    public float AttackSpeed => _attackSpeed;
    public float skillCooldown => _skillCooldown;

    public static KaijuUpgradeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void BuyUpgrade(UpgradeType upgradeType, float amount)
    {
        switch (upgradeType)    
        {
            case UpgradeType.Damage: _damage += amount; return;
            case UpgradeType.Range: _range += amount; return;
            case UpgradeType.Speed: _speed += amount; return;
            case UpgradeType.AttackSpeed: _attackSpeed += amount; return;
            case UpgradeType.Health: _maxHealth += amount; return;
            default: return;
        }
    }
}

