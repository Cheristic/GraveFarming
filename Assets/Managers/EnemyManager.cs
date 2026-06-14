using System;
using System.Collections.Generic;
using System.Collections;
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

        RoundManager.TriggerActivePhase += SpawnEnemies;
        RoundManager.TriggerRestPhase += ChooseNewSpawnerPositions;
    }

    private void OnDisable()
    {
        RoundManager.TriggerActivePhase -= SpawnEnemies;
        RoundManager.TriggerRestPhase -= ChooseNewSpawnerPositions;
    }
    
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
            spawnPos = GridManager.Instance.ToWorldSpace(GridManager.Instance.RandomGridPos());
            //spawnPos = GridManager.Instance.NewRandomPos(GridManager.Instance.ToWorldSpace(pos), new Vector2(2 * cellSize / 3, 2 * cellSize / 3));
        } while (!ValidSpawnerPosition(spawnPos));

        spawner.gameObject.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0);
    }

    private void ChooseNewSpawnerPositions()
    {
        foreach (EnemySpawner spawner in spawnerList)
        {
            SetSpawnPosition(spawner);
        }
    }
    private void SpawnEnemies()
    {
        List<Enemy> spawned = new();
        foreach (EnemySpawner spawner in spawnerList)
        {
            spawned.AddRange(spawner.SpawnEnemies());
        }

        StartCoroutine(CheckEnemyStatus());

        IEnumerator CheckEnemyStatus()
        {
            bool enemiesAlive = true;
            while (RoundManager.Instance.roundActive && enemiesAlive)
            {
                yield return null;
                enemiesAlive = false;
                foreach (var enemy in spawned)
                {
                    if (!enemy.hasSpawned || enemy.isAlive) {
                        enemiesAlive = true;
                        break;
                    }
                }
            }

            if (RoundManager.Instance.roundActive)
            {
                RoundManager.Instance.BeginNextRound();
            }
        }
    }
}