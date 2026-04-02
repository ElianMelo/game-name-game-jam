using UnityEngine;

public class KaijuUpgradeManager : MonoBehaviour
{
    public float maxHealth;
    public float healthRegen;
    public float damage;
    public float attackSpeed;
    public float skillCooldown;

    public static KaijuUpgradeManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
