using UnityEngine;
using System.Collections.Generic;
using System;


public class GraveDatabase : MonoBehaviour
{
    public enum GraveType 
    {
        Shooter,
        Blocker
    }
    public enum Resources
    {
        SoulPieces,
        GravePieces
    }

    [Serializable]
    public class ResourceRequirements
    {
        public Resources type;
        public int cost;
    }

    [Serializable]
    public class GraveData
    {
        public GraveType type;
        public ResourceRequirements[] resourceRequirements;
        public Sprite sprite;
        public GameObject gravePrefab;
    }

    public List<GraveData> GraveList = new();

    public static GraveDatabase Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    public GraveData GetGraveData(GraveType type) => GraveList[(int)type];
    public GraveData GetGraveData(int index) => GraveList[index];

    public static int CompareResourceRequirements(ResourceRequirements req1, ResourceRequirements req2)
    {
        if ((int)req1.type <= (int)req2.type) return -1;
        else if ((int)req1.type >= (int)req2.type) return 1;

        return 0;
    }

    private void Start()
    {
        foreach (GraveData data in GraveList)
        {
            Array.Sort(data.resourceRequirements, CompareResourceRequirements);
        }
    }

}
