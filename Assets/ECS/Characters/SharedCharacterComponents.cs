using Unity.Entities;
using UnityEngine;

public struct Health : IComponentData
{
    public float Value;
}

public struct EnemyTag : IComponentData { }

public struct GraveComponent : IComponentData
{
    public int numGravePieces;
}