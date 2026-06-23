using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// takes from https://github.com/UnityTechnologies/AngryBots_ECS/blob/master/AngryDOTS/Assets/Scripts/DOTS/Systems/CollisionSystem.cs#L44

[BurstCompile]
[UpdateAfter(typeof(ShooterBulletSystem))]
public partial struct CollisionSystem : ISystem
{
    EntityQuery bulletQuery;
    EntityQuery enemyQuery;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ProjectileComponent>();

        bulletQuery = SystemAPI.QueryBuilder().WithAll<ProjectileComponent, LocalTransform>().Build();
        enemyQuery = SystemAPI.QueryBuilder().WithAll<EnemyTag, Health, LocalTransform>().Build();
    }

    public void OnUpdate(ref SystemState state)
    {
        ProjectileToEnemyCollisionJob jobProjECollision = new ProjectileToEnemyCollisionJob()
        {
            radius = Settings.EnemyCollisionRadius * Settings.EnemyCollisionRadius,
            bulletTrans = bulletQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob),
            bulletData = bulletQuery.ToComponentDataArray<ProjectileComponent>(Allocator.TempJob)
        };

        state.Dependency = jobProjECollision.ScheduleParallel(enemyQuery, state.Dependency);
    }

    public partial struct ProjectileToEnemyCollisionJob : IJobEntity
    {
        public float radius;

        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<LocalTransform> bulletTrans;
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<ProjectileComponent> bulletData;

        public void Execute(ref Health health, in LocalTransform localTransform)
        {
            float damage = 0f;
            for (int i = 0; i < bulletTrans.Length; i++)
            {
                if (CheckCollisionDistance(localTransform.Position, bulletTrans[i].Position, radius))
                {
                    damage += bulletData[i].DAMAGE;
                }
            }

            health.Value -= damage;
        }

        bool CheckCollisionDistance(float3 a, float3 b, float radius)
        {
            float3 delta = a - b;
            float dist = delta.x * delta.x + delta.y + delta.y;
            return dist <= radius;
        }
    }
}
