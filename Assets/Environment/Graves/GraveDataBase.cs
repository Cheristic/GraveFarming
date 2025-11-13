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
    public class GraveData
    {
        public GraveType type;
        public Resources[] resourceRequirements;
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
}
