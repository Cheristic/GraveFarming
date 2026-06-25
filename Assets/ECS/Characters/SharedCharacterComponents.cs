using Unity.Entities;
using UnityEngine;

public struct Health : IComponentData
{
    public float Value;
}

public struct EnemyTag : IComponentData {}

public struct MoveSpeed : IComponentData
{
    public float Value;
}

public struct AttackRange : IComponentData
{
    public float Value;
}

public struct GraveComponent : IComponentData
{
    public int numGravePieces;
}