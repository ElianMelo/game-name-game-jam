using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private List<Transform> spawnPoints = new();

    private float currentSpawnTime = 0;

    private void Start()
    {
        currentSpawnTime = spawnTime;
    }

    void Update()
    {
        SpawnUnit();
    }

    private void SpawnUnit()
    {
        if (GameManager.Instance.CurrentState != GameState.KaijuControl) return;
        currentSpawnTime -= Time.deltaTime;
        if (currentSpawnTime > 0) return;
        currentSpawnTime = spawnTime;
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
    }
}
