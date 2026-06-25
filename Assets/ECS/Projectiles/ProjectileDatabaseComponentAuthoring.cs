using Unity.Entities;
using UnityEngine;

public class ProjectileDatabaseComponentAuthoring : MonoBehaviour
{
    public GameObject shooterBulletPrefab;

    public class Baker : Baker<ProjectileDatabaseComponentAuthoring>
    {
        public override void Bake(ProjectileDatabaseComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ProjectileDatabaseComponent
            {
                shooterBulletEntity = GetEntity(authoring.shooterBulletPrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct ProjectileDatabaseComponent : IComponentData
{
    public Entity shooterBulletEntity;
}
