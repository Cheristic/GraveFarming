using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
public partial struct EnemyDeathCheckSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        using (var commandBuffer = new EntityCommandBuffer(Allocator.TempJob))
        {

            foreach (var (health, entity) in SystemAPI.Query<RefRO<Health>>().WithAll<EnemyTag>().WithEntityAccess())
            {

                if (health.ValueRO.Value <= 0f)
                {
                    commandBuffer.DestroyEntity(entity);
                }
            }

            commandBuffer.Playback(state.EntityManager);
        }
    }
}