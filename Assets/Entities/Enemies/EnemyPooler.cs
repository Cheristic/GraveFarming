using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

using Enemy = Entity;

public class EnemyPooler : MonoBehaviour
{
    List<List<GameObject>> EnemyPools;
    [SerializeField] int PrespawnEnemyAmount;

    private void Start()
    {
        EnemyPools = new();

        for (int i = 0; i < EnemyDataBase.Instance.EnemyList.Count; i++)
        {
            List<GameObject> EnemyPool = new();

            for (int j = 0; j < PrespawnEnemyAmount; j++)
            {
                Enemy enemy = Instantiate(EnemyDataBase.Instance.EnemyList[i].enemyPrefab, this.transform).GetComponent<Enemy>();
                enemy.gameObject.SetActive(false);
                enemy.Init();
                EnemyPool.Add(enemy.gameObject);
            }

            EnemyPools.Add(EnemyPool);
        }
    }

    public Enemy GetEnemy(EnemyDataBase.EnemyType type)
    {
        List<GameObject> pool = EnemyPools[(int)type];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i].gameObject.GetComponent<Enemy>();
            }
        }

        Enemy enemy = Instantiate(EnemyDataBase.Instance.EnemyList[(int)type].enemyPrefab, this.transform).GetComponent<Enemy>();
        enemy.gameObject.SetActive(false);
        enemy.Init();
        pool.Add(enemy.gameObject);
        return enemy;
    }
}
