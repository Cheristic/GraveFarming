using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct ShooterBulletSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach ((RefRO<ProjectileComponent> proj, RefRW<LocalTransform> localTransform)
            in SystemAPI.Query<RefRO<ProjectileComponent>, RefRW<LocalTransform>>())
        {
            localTransform.ValueRW = localTransform.ValueRW.Translate(proj.ValueRO.MOVE_SPEED
                * SystemAPI.Time.DeltaTime * new float3(1f, 1f, 0f));
        }
    }
}