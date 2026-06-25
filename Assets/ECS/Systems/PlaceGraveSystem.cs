using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial class PlaceGraveSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<GraveDatabaseComponent>();
    }

    protected override void OnUpdate()
    {
        if (PlayerPlaceGraves.Instance.spawnNewGrave)
        {
            GraveDatabaseComponent database = SystemAPI.GetSingleton<GraveDatabaseComponent>();

            Entity spawnedGrave = EntityManager.Instantiate(database.shooterGraveEntity);

            SystemAPI.SetComponent(spawnedGrave, new LocalTransform
            {
                Position = new float3(PlayerPlaceGraves.Instance.graveSpawnLocation.x, PlayerPlaceGraves.Instance.graveSpawnLocation.y, 0),
                Scale = 0.5f,
                Rotation = quaternion.identity
            });

            PlayerPlaceGraves.Instance.spawnNewGrave = false;
        }
    }
}