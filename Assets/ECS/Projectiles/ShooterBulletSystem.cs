using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct ShooterBulletSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ProjectileComponent>();
    }

    public void OnUpdate(ref SystemState state)
    {
        //foreach ((RefRO<ProjectileComponent> proj, RefRW<LocalTransform> localTransform)
        //    in SystemAPI.Query<RefRO<ProjectileComponent>, RefRW<LocalTransform>>())
        //{
        //    localTransform.ValueRW = localTransform.ValueRO.Translate(proj.ValueRO.MOVE_SPEED
        //        * SystemAPI.Time.DeltaTime * new float3(1f, 1f, 0f));
        //}

        ShooterBulletMoveJob moveJob = new ShooterBulletMoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        state.Dependency = moveJob.ScheduleParallel(state.Dependency);
    }

    public partial struct ShooterBulletMoveJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(in ProjectileComponent proj, ref LocalTransform localTransform)
        {
            localTransform = localTransform.Translate(proj.MOVE_SPEED
                * deltaTime * proj.dir);
        }
    }
}