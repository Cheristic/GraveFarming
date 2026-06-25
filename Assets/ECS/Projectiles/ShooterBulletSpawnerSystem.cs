using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static ShooterGraveTargetCheckSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;


public partial class ShooterBulletSpawnerSystem : SystemBase
{


    protected override void OnCreate()
    {
        RequireForUpdate<ProjectileDatabaseComponent>();
    }

    protected override void OnUpdate()
    {
       
        foreach (var (grave, transform) in SystemAPI.Query<RefRW<ShooterGraveComponent>, RefRO<LocalTransform>>())
        {
            if (!grave.ValueRO.triggerShoot) continue;

            SpawnBullet(transform.ValueRO.Position, grave.ValueRO.targetPos);

            grave.ValueRW.triggerShoot = false;

        }
        //for (int i = 0; i < PoolManager.Instance.gravePooler.ShooterGravePool.Count; i++)
        //{
        //    if (((ShooterGrave)PoolManager.Instance.gravePooler.ShooterGravePool[i]).health > 0 &&
        //        ((ShooterGrave)PoolManager.Instance.gravePooler.ShooterGravePool[i]).triggerShoot)
        //    {
        //        SpawnBullet(((ShooterGrave)PoolManager.Instance.gravePooler.ShooterGravePool[i]).transform.position,
        //            ((ShooterGrave)PoolManager.Instance.gravePooler.ShooterGravePool[i]).target.transform.position);
        //        ((ShooterGrave)PoolManager.Instance.gravePooler.ShooterGravePool[i]).triggerShoot = false;
        //    }
        //}
    }

    public void SpawnBullet(float3 sourcePos, float3 targetPos)
    {
        ProjectileDatabaseComponent spawner = SystemAPI.GetSingleton<ProjectileDatabaseComponent>();

        Entity spawnedBullet = EntityManager.Instantiate(spawner.shooterBulletEntity);

        float3 dir = targetPos - sourcePos;
        SystemAPI.SetComponent(spawnedBullet, new LocalTransform
        {
            Position = new float3(sourcePos.x, sourcePos.y, sourcePos.z),
            Scale = 0.5f,
            Rotation = quaternion.EulerXYZ(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)
        });

        RefRW<ProjectileComponent> proj = SystemAPI.GetComponentRW<ProjectileComponent>(spawnedBullet);
        proj.ValueRW.dir = new float3(dir.x, dir.y, 0);
    }

    
}