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

    internal int spawnNewEnemy;
    IEnumerator DelayedSpawns(int toSpawn)
    {
        if (GridManager.Instance.HasGraveAt(transform.position))
        {
            GridManager.Instance.RemoveGrave(transform.position);
        }


        for (int i = 0; i < toSpawn; i++)
        {
            spawnNewEnemy++;
            //enemy.Spawn(new Vector2(transform.position.x, transform.position.y));
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void SpawnEnemies()
    {
        int toSpawn = 0;
        for (int i = 0; i < spawnAmount; i++)
        {
            System.Random random = new(RoundManager.Instance.roundNum * spawnAmount + i);
            int type = random.Next(2);
            //Enemy enemy = PoolManager.Instance.enemyPooler.GetEnemy((EnemyDataBase.EnemyType)type);
            //enemy.isAlive = true;
            //enemy.hasSpawned = false;
            toSpawn++;
        }
        spawnNewEnemy = toSpawn;
        //StartCoroutine(DelayedSpawns(toSpawn));
    }
}