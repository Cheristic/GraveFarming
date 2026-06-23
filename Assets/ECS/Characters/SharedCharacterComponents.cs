using Unity.Entities;

public struct Health : IComponentData
{
    public float Value;
}

public struct EnemyTag : IComponentData { }