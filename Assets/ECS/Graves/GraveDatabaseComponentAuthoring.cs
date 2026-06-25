using Unity.Entities;
using UnityEngine;

public class GraveDatabaseComponentAuthoring : MonoBehaviour
{
    public GameObject shooterGravePrefab;

    public class Baker : Baker<GraveDatabaseComponentAuthoring>
    {
        public override void Bake(GraveDatabaseComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new GraveDatabaseComponent
            {
                shooterGraveEntity = GetEntity(authoring.shooterGravePrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct GraveDatabaseComponent : IComponentData
{
    public Entity shooterGraveEntity;
}
