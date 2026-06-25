
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using static CollisionSystem;

[BurstCompile]
public partial struct ShooterGraveTargetCheckSystem : ISystem
{
    EntityQuery enemyQuery;
    EntityQuery shooterGraveQuery;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ShooterGraveComponent>();
        state.RequireForUpdate<EnemyTag>();
        enemyQuery = SystemAPI.QueryBuilder().WithAll<EnemyTag, Health, LocalTransform>().Build();
        shooterGraveQuery = SystemAPI.QueryBuilder().WithAll<ShooterGraveComponent, LocalTransform>().Build();
    }

    public void OnUpdate(ref SystemState state)
    {
        //foreach ((RefRO<ProjectileComponent> proj, RefRW<LocalTransform> localTransform)
        //    in SystemAPI.Query<RefRO<ProjectileComponent>, RefRW<LocalTransform>>())
        //{
        //    localTransform.ValueRW = localTransform.ValueRO.Translate(proj.ValueRO.MOVE_SPEED
        //        * SystemAPI.Time.DeltaTime * new float3(1f, 1f, 0f));
        //}

        ShooterGraveTargetCheckJob jobTarget = new ShooterGraveTargetCheckJob
        {
            enemyTrans = enemyQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob),
            playerPos = new float3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, 0),
            deltaTime = SystemAPI.Time.DeltaTime
        };

        state.Dependency = jobTarget.ScheduleParallel(shooterGraveQuery, state.Dependency);
    }

    public partial struct ShooterGraveTargetCheckJob : IJobEntity
    {
        [DeallocateOnJobCompletion]
        [ReadOnly] public NativeArray<LocalTransform> enemyTrans;

        public float3 playerPos;
        public float deltaTime;
        public void Execute(ref ShooterGraveComponent grave, in LocalTransform localTransform)
        {
            if (grave.TIMER_isShooting)
            {
                if (grave.timer > 0)
                {
                    grave.timer -= deltaTime;
                    return;
                } else
                {
                    if (grave.bulletsShot == grave.BULLETS_TO_SHOOT)
                    {
                        grave.TIMER_isShooting = false;
                        grave.bulletsShot = 0;
                    } else
                    {
                        grave.bulletsShot++;
                        grave.timer = grave.bulletsShot == grave.BULLETS_TO_SHOOT ? grave.REST_TIME_AFTER_BULLETS :
                            grave.TIME_BETWEEN_BULLETS;
                        grave.triggerShoot = true;
                        return;
                    }
                }
            } else if (grave.TIMER_foundNoTarget)
            {
                if (grave.timer < 0)
                {
                    grave.TIMER_foundNoTarget = false;
                    grave.timer = 0;
                }
                else
                {
                    grave.timer -= deltaTime;
                    return;
                }
            }
            

            bool foundTarget = false;
            float bestPlayerToEnemyDist = grave.PLAYER_CHECK_FOR_ENEMY_RADIUS;
            for (int i = 0; i < enemyTrans.Length; i++)
            {
                float graveToEnemyDist = CheckDistance(localTransform.Position, enemyTrans[i].Position);
                float playerToEnemyDist = CheckDistance(playerPos, enemyTrans[i].Position);


                if (graveToEnemyDist < grave.SHOOTING_RADIUS && playerToEnemyDist < bestPlayerToEnemyDist)
                {
                    grave.enemies = graveToEnemyDist;
                    grave.playerdist = playerToEnemyDist;
                    bestPlayerToEnemyDist = playerToEnemyDist;
                    grave.targetPos = enemyTrans[i].Position;
                    foundTarget = true;
                }
            }
            if (!foundTarget)
            {
                float bestGraveToEnemyDist = grave.SHOOTING_RADIUS;
                for (int i = 0; i < enemyTrans.Length; i++)
                {
                    float graveToEnemyDist = CheckDistance(localTransform.Position, enemyTrans[i].Position);

                    if (graveToEnemyDist < bestGraveToEnemyDist)
                    {
                        grave.enemies = graveToEnemyDist;
                        bestGraveToEnemyDist = graveToEnemyDist;
                        grave.targetPos = enemyTrans[i].Position;
                        foundTarget = true;
                    }
                }
            }

            if (foundTarget)
            {
                grave.TIMER_isShooting = true;
                grave.timer = 0;
            } else
            {
                grave.TIMER_foundNoTarget = true;
                grave.timer = grave.CHECK_FOR_ENEMY_INTERVAL;
            }
        }

        float CheckDistance(float3 a, float3 b)
        {
            float3 delta = a - b;
            float dist = math.sqrt(delta.x * delta.x + delta.y + delta.y);
            return dist;
        }
    }
}