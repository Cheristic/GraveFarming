using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField] int enemySpawnAmount;
    [SerializeField] double minTilesDistanceForSpawn;
    [SerializeField] public List<EnemySpawner> spawnerList;

    Vector2Int gridDimensions { get => GridManager.Instance.GridDimensions; }

    public static EnemyManager Main { get; private set; }
    private void Awake()
    {
        if (Main != null && Main != this) Destroy(this);
        else Main = this;
    }

    private void OnEnable()
    {
        RoundManager.NewRoundStarted += SpawnEnemies;
    }
    

    float cellSize { get => 1.0f / GridManager.Instance.WorldtoGridRatio; }
    void SetSpawnPosition(EnemySpawner spawner)
    {
        bool ValidSpawnerPosition(Vector2 pos)
        {
            Vector2 playerPos = PlayerManager.Instance.gameObject.transform.position;
            if (Vector2.Distance(pos, playerPos) <= minTilesDistanceForSpawn) return false;
            return true;
        }
        
        Vector2 spawnPos = new();
        do
        {
            Vector2Int pos = GridManager.Instance.RandomGridPos();
            spawnPos = GridManager.Instance.NewRandomPos(GridManager.Instance.ToWorldSpace(pos), new Vector2(2 * cellSize / 3, 2 * cellSize / 3));
        } while (!ValidSpawnerPosition(spawnPos));

        spawner.gameObject.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
    }

    private void SpawnEnemies(Component component)
    {
        foreach (EnemySpawner spawner in spawnerList)
        {
            SetSpawnPosition(spawner);
            spawner.SpawnEnemies();
        }
    }
}