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

        ShooterBulletMoveJob moveJob = new ShooterBulletMoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        state.Dependency = moveJob.ScheduleParallel(state.Dependency);
    }

    public partial struct ShooterBulletMoveJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(ref ProjectileComponent proj, ref LocalTransform localTransform)
        {
            localTransform = localTransform.Translate(proj.MOVE_SPEED
                * deltaTime * proj.dir);

            proj.TIME_OUT -= deltaTime;


        }
    }
}