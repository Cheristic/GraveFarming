using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnAmount;
    [SerializeField] float spawnDelay;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnEnemies();
        }
#endif
    }
    
    IEnumerator DelayedSpawns(List<Enemy> enemies)
    {
        if (GridManager.Instance.HasGraveAt(transform.position))
        {
            GridManager.Instance.RemoveGrave(transform.position);
        }

        foreach (var enemy in enemies)
        {
            enemy.Spawn(new Vector2(transform.position.x, transform.position.y));
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public List<Enemy> SpawnEnemies()
    {
        List<Enemy> enemies = new();
        for (int i = 0; i < spawnAmount; i++)
        {
            int type = Random.Range(0, 2);
            Enemy enemy = PoolManager.Instance.enemyPooler.GetEnemy((EnemyDataBase.EnemyType)type);
            enemy.hasSpawned = false;
            enemies.Add(enemy);
        }
        StartCoroutine(DelayedSpawns(enemies));
        return enemies;
    }
}