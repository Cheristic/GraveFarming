using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    List<List<Enemy>> EnemyPools;
    [SerializeField] int PrespawnEnemyAmount;

    private void Start()
    {
        EnemyPools = new();

        for (int i = 0; i < EnemyDataBase.Instance.EnemyList.Count; i++)
        {
            List<Enemy> EnemyPool = new();

            for (int j = 0; j < PrespawnEnemyAmount; j++)
            {
                Enemy enemy = Instantiate(EnemyDataBase.Instance.EnemyList[i].enemyPrefab, this.transform).GetComponent<Enemy>();
                enemy.gameObject.SetActive(false);
                enemy.Init(EnemyDataBase.Instance.EnemyList[i]);
                EnemyPool.Add(enemy);
            }

            EnemyPools.Add(EnemyPool);
        }
    }

    public Enemy GetEnemy(EnemyDataBase.EnemyType type)
    {
        List<Enemy> pool = EnemyPools[(int)type];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].isAlive)
            {
                return pool[i];
            }
        }

        Enemy enemy = Instantiate(EnemyDataBase.Instance.EnemyList[(int)type].enemyPrefab, this.transform).GetComponent<Enemy>();
        enemy.gameObject.SetActive(false);
        enemy.Init(EnemyDataBase.Instance.EnemyList[(int)type]);
        pool.Add(enemy);
        return enemy;
    }
}
