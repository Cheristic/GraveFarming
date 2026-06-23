using Unity.Entities;
using UnityEngine;

public class ShooterGraveComponentAuthoring : MonoBehaviour
{
    public GameObject shooterBulletPrefab;

    public class Baker : Baker<ShooterGraveComponentAuthoring>
    {
        public override void Bake(ShooterGraveComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ProjectileDatabaseComponent
            {
                shooterBulletEntity = GetEntity(authoring.shooterBulletPrefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct ShooterGraveComponent
{

}