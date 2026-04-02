using UnityEngine;

public class TroopUpgradeManager : MonoBehaviour
{
    public float hp;
    public float damage;
    public float attackSpeed;
    public float amountOfSoldiers;
    public float amountOfGroups;

    public static TroopUpgradeManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
