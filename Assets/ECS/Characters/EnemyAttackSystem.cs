using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyAttackSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemyTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        if (PlayerManager.Instance == null)
            return;

        float3 playerPos = new float3(
            PlayerManager.Instance.transform.position.x,
            PlayerManager.Instance.transform.position.y,
            0f
        );

        foreach (var (enemyTransform, attackrange) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<AttackRange>>().WithAll<EnemyTag>())
        {
            float3 delta = playerPos - enemyTransform.ValueRO.Position;
            delta.z = 0f;

            if (math.lengthsq(delta) <= attackrange.ValueRO.Value * attackrange.ValueRO.Value)
            {
                PlayerManager.Instance.gameObject.SetActive(false);
                return;
            }
        }
    }
}