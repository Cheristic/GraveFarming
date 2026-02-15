using Unity.VisualScripting;
using UnityEngine;
using Enemy = Entity;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnAmount;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnEnemies();
        }

    }
    
    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int type = Random.Range(0, 1);
            Enemy enemy = PoolManager.Instance.enemyPooler.GetEnemy((EnemyDataBase.EnemyType)type);
            enemy.Spawn(new Vector2(transform.position.x, transform.position.y));
        }
    }
}