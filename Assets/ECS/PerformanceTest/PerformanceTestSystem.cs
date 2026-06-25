using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

[BurstCompile]
public partial class PerformanceTestSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<GraveDatabaseComponent>();
        if (SceneManager.GetActiveScene().name != "PerformanceTest") this.Enabled = false;
    }

    protected override void OnUpdate()
    {
        this.Enabled = false;
        for (int x = 0; x < GridManager.Instance.GridDimensions.x; x++)
            for (int y = 0; y < GridManager.Instance.GridDimensions.y; y++)
            {
                if (x == GridManager.Instance.GridDimensions.x / 2 && y == GridManager.Instance.GridDimensions.y / 2) continue;

                Vector2 graveLocation = GridManager.Instance.PlaceGrave(GridManager.Instance.ToWorldSpace(new Vector2Int(x, y)));

                GraveDatabaseComponent database = SystemAPI.GetSingleton<GraveDatabaseComponent>();

                Entity spawnedGrave = EntityManager.Instantiate(database.shooterGraveEntity);

                SystemAPI.SetComponent(spawnedGrave, new LocalTransform
                {
                    Position = new float3(graveLocation.x, graveLocation.y, 0),
                    Scale = 0.5f,
                    Rotation = quaternion.identity
                });
            }
    }
}