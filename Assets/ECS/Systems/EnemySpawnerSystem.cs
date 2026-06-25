using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial class EnemySpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<EnemyDatabaseComponent>();
    }

    protected override void OnUpdate()
    {
        foreach (var spawner in EnemyManager.Main.spawnerList)
        {
            while (spawner.spawnNewEnemy > 0)
            {
                EnemyDatabaseComponent database = SystemAPI.GetSingleton<EnemyDatabaseComponent>();

                Entity spawnedEnemy = EntityManager.Instantiate(database.enemyEntity);

                SystemAPI.SetComponent(spawnedEnemy, new LocalTransform
                {
                    Position = new float3(spawner.transform.position.x, spawner.transform.position.y, 0),
                    Scale = 1.0f,
                    Rotation = quaternion.identity
                });

                spawner.spawnNewEnemy--;
            }
        }
    }
}