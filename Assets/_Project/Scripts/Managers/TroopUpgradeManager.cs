using System;
using UnityEngine;

public enum TroopName
{
    Soldier,
    Rider,
    Crossbow,
    Catapult
}

[Serializable]
public class TroopAttributesGroup
{
    public TroopName troopName;
    public EnemyType troopType;
    public bool unlocked = false;
    public float health;
    public float damage;
    public float speed;
    public float range;
    public float attackSpeed;
    public float spawnAmount;
    public float spawnSpeed;
}

public class TroopUpgradeManager : MonoBehaviour
{
    [SerializeField] private TroopAttributesGroup _soldier;
    [SerializeField] private TroopAttributesGroup _rider;
    [SerializeField] private TroopAttributesGroup _crossbow;
    [SerializeField] private TroopAttributesGroup _catapult;

    public TroopAttributesGroup Soldier => _soldier;
    public TroopAttributesGroup Rider => _rider;
    public TroopAttributesGroup Crossbow => _crossbow;
    public TroopAttributesGroup Catapult => _catapult;

    public TroopAttributesGroup GetTroopByName(TroopName troopName)
    {   
        switch (troopName)
        {
            case TroopName.Soldier: return Soldier;
            case TroopName.Rider: return Rider;
            case TroopName.Crossbow: return Crossbow;
            case TroopName.Catapult: return Catapult;
            default: return null;
        }
    }

    public static TroopUpgradeManager Instance;

    private void Awake()
    {
        Instance = this;
        _soldier.unlocked = true;
    }

    public void BuyUpgrade(UpgradeType upgradeType, TroopName troopName, float amount)
    {
        if(upgradeType == UpgradeType.Unlock)
        {
            switch (troopName)
            {
                case TroopName.Rider: _rider.unlocked = true; break;
                case TroopName.Crossbow: _crossbow.unlocked = true; break;
                case TroopName.Catapult: _catapult.unlocked = true; break;
            }
            UpgradeTreeSwitcher.UnlockTroopTreePath?.Invoke(troopName);
        }
        switch (troopName)
        {
            case TroopName.Soldier:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _soldier.damage += amount; return;
                    case UpgradeType.Range: _soldier.range += amount; return;
                    case UpgradeType.Speed: _soldier.speed += amount; return;
                    case UpgradeType.AttackSpeed: _soldier.attackSpeed += amount; return;
                    case UpgradeType.Health: _soldier.health += amount; return;
                    case UpgradeType.SpawnAmount: _soldier.spawnAmount += amount; return;
                    case UpgradeType.SpawnSpeed: _soldier.spawnSpeed += amount; return;
                    default: return;
                }
            case TroopName.Rider:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _rider.damage += amount; return;
                    case UpgradeType.Range: _rider.range += amount; return;
                    case UpgradeType.Speed: _rider.speed += amount; return;
                    case UpgradeType.AttackSpeed: _rider.attackSpeed += amount; return;
                    case UpgradeType.Health: _rider.health += amount; return;
                    case UpgradeType.SpawnAmount: _rider.spawnAmount += amount; return;
                    case UpgradeType.SpawnSpeed: _rider.spawnSpeed += amount; return;
                    default: return;
                }
            case TroopName.Crossbow:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _crossbow.damage += amount; return;
                    case UpgradeType.Range: _crossbow.range += amount; return;
                    case UpgradeType.Speed: _crossbow.speed += amount; return;
                    case UpgradeType.AttackSpeed: _crossbow.attackSpeed += amount; return;
                    case UpgradeType.Health: _crossbow.health += amount; return;
                    case UpgradeType.SpawnAmount: _crossbow.spawnAmount += amount; return;
                    case UpgradeType.SpawnSpeed: _crossbow.spawnSpeed += amount; return;
                    default: return;
                }
            case TroopName.Catapult:
                switch (upgradeType)
                {
                    case UpgradeType.Damage: _catapult.damage += amount; return;
                    case UpgradeType.Range: _catapult.range += amount; return;
                    case UpgradeType.Speed: _catapult.speed += amount; return;
                    case UpgradeType.AttackSpeed: _catapult.attackSpeed += amount; return;
                    case UpgradeType.Health: _catapult.health += amount; return;
                    case UpgradeType.SpawnAmount: _catapult.spawnAmount += amount; return;
                    case UpgradeType.SpawnSpeed: _catapult.spawnSpeed += amount; return;
                    default: return;
                }
            default: break;
        }

        
    }
}
