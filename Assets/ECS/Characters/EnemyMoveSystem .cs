using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct EnemyMoveSystem : ISystem
{
    EntityQuery enemyQuery;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyTag>();

        enemyQuery = SystemAPI.QueryBuilder().WithAll<EnemyTag, Health, MoveSpeed, LocalTransform>().Build();
    }

    public void OnUpdate(ref SystemState state)
    {
        float3 playerPos = new float3(
            PlayerManager.Instance.transform.position.x,
            PlayerManager.Instance.transform.position.y,
            0f
        );

        EnemyMoveJob moveJob = new EnemyMoveJob
        {
            playerPos = playerPos,
            deltaTime = SystemAPI.Time.DeltaTime
        };

        state.Dependency = moveJob.ScheduleParallel(enemyQuery, state.Dependency);
    }

    [BurstCompile]
    public partial struct EnemyMoveJob : IJobEntity
    {
        public float3 playerPos;
        public float deltaTime;

        public void Execute(ref LocalTransform transform, in EnemyTag enemyTag, in Health health, in MoveSpeed moveSpeed)
        {
            float movespeed = moveSpeed.Value;

            float3 enemyPos = transform.Position;

            float3 direction = playerPos - enemyPos;
            direction.z = 0f;

            if (math.lengthsq(direction) > 0.0001f)
            {
                direction = math.normalize(direction);

                transform.Position += direction * movespeed * deltaTime;
            }
        }
    }
}