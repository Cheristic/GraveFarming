using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GraveDatabase;

public class EnemyDataBase : MonoBehaviour
{
    public enum EnemyType
    {
        EnemyType1,
        EnemyType2
    }

    public enum ResourceDrops
    {
        SoulPieces,

    }

    [Serializable]
    public class EnemyData
    {
        public EnemyType type;
        public ResourceDrops[] resourceDrops;
        public Sprite sprite;
        public GameObject enemyPrefab;
    }

    public List<EnemyData> EnemyList = new();

    public static EnemyDataBase Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public EnemyData GetEnemyData(EnemyType type) => EnemyList[(int)type];
    public EnemyData GetEnemyData(int index) => EnemyList[index];
}
