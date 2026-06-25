using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
public partial struct ShooterBulletTimeoutSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ProjectileComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
        using (var commandBuffer = new EntityCommandBuffer(Allocator.TempJob))
        {

            foreach (var (proj, entity) in SystemAPI.Query<RefRO<ProjectileComponent>>().WithEntityAccess())
            {

                if (proj.ValueRO.TIME_OUT <= 0f)
                {
                    commandBuffer.DestroyEntity(entity);
                }
            }

            commandBuffer.Playback(state.EntityManager);
        }
    }
}