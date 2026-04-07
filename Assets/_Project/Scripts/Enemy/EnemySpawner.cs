using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject riderPrefab;
    [SerializeField] private GameObject crossbowPrefab;
    [SerializeField] private GameObject catapultPrefab;
    [SerializeField] private List<Transform> spawnPoints = new();

    private float currentSpawnTime = 0;

    private void Start()
    {
        currentSpawnTime = 1 / TroopUpgradeManager.Instance.Soldier.spawnSpeed;
    }

    void Update()
    {
        SpawnUnit(TroopName.Soldier);
        SpawnUnit(TroopName.Rider);
        SpawnUnit(TroopName.Crossbow);
        SpawnUnit(TroopName.Catapult);
    }

    private void SpawnUnit(TroopName troopName)
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        TroopAttributesGroup currentGroup = GetUnitBasedOnName(troopName);
        if (currentGroup.unlocked == false) return;
        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime > 0) return;
        currentSpawnTime = 1 / currentGroup.spawnSpeed;
        for (int i = 0; i < currentGroup.spawnAmount; i++)
        {
            GameObject enemyObj = Instantiate(GetPrefabBasedOnName(troopName), spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        }
    }

    private GameObject GetPrefabBasedOnName(TroopName troopName)
    {
        switch (troopName)
        {
            case TroopName.Soldier: return soldierPrefab;
            case TroopName.Rider: return soldierPrefab;
            case TroopName.Crossbow: return crossbowPrefab;
            case TroopName.Catapult: return crossbowPrefab;
        }
        return null;
    }

    private TroopAttributesGroup GetUnitBasedOnName(TroopName troopName)
    {
        switch (troopName)
        {
            case TroopName.Soldier: return TroopUpgradeManager.Instance.Soldier;
            case TroopName.Rider: return TroopUpgradeManager.Instance.Rider;
            case TroopName.Crossbow: return TroopUpgradeManager.Instance.Crossbow;
            case TroopName.Catapult: return TroopUpgradeManager.Instance.Catapult;
        }
        return null;
    }
}
