using UnityEngine;

public class TroopUpgradeManager : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _amountOfTroop;
    [SerializeField] private float _amountOfGroups;

    public float Health => _health;
    public float Damage => _damage;
    public float Speed => _speed;
    public float Range => _range;
    public float AttackSpeed => _attackSpeed;
    public float AmountOfTroop => _amountOfTroop;
    public float AmountOfGroups => _amountOfGroups;

    public static TroopUpgradeManager Instance;

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
            case UpgradeType.Health: _health += amount; return;
            case UpgradeType.AmountTroop: _amountOfTroop += amount; return;
            case UpgradeType.AmountGroup: _amountOfGroups += amount; return;
            default: return;
        }
    }
}
