using Unity.Entities;
using UnityEngine;

public class EnemyDatabaseComponentAuthoring : MonoBehaviour
{
    public GameObject enemyPrefab;

    public class Baker : Baker<EnemyDatabaseComponentAuthoring>
    {
        public override void Bake(EnemyDatabaseComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemyDatabaseComponent
            {
                enemyEntity = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct EnemyDatabaseComponent : IComponentData
{
    public Entity enemyEntity;
}
