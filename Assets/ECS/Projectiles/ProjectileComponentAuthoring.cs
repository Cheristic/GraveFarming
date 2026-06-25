using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class ProjectileComponentAuthoring : MonoBehaviour
{
    //public float3 Dir;
    public float MOVE_SPEED;
    public float TIME_OUT;
    public float DAMAGE;

    private class Baker : Baker<ProjectileComponentAuthoring>
    {
        public override void Bake(ProjectileComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileComponent
            {
                MOVE_SPEED = authoring.MOVE_SPEED,
                TIME_OUT = authoring.TIME_OUT,
                DAMAGE = authoring.DAMAGE
            });
        }
    }
}

public struct ProjectileComponent : IComponentData
{
    public float MOVE_SPEED;
    public float TIME_OUT;
    public float DAMAGE;
    public float3 dir;
}
