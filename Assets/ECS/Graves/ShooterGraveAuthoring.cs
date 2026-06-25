using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ShooterGraveAuthoring : MonoBehaviour
{
    public float PLAYER_CHECK_FOR_ENEMY_RADIUS;
    public float SHOOTING_RADIUS;
    public int BULLETS_TO_SHOOT = 3;
    public float CHECK_FOR_ENEMY_INTERVAL = .5f;
    public float TIME_BETWEEN_BULLETS = .2f;
    public float REST_TIME_AFTER_BULLETS = 1f;

    public int GRAVE_PIECES_TO_DROP;
    public int HEALTH;


    public class Baker : Baker<ShooterGraveAuthoring>
    {
        public override void Bake(ShooterGraveAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ShooterGraveComponent
            {
                PLAYER_CHECK_FOR_ENEMY_RADIUS = authoring.PLAYER_CHECK_FOR_ENEMY_RADIUS,
                SHOOTING_RADIUS = authoring.SHOOTING_RADIUS,
                BULLETS_TO_SHOOT = authoring.BULLETS_TO_SHOOT,
                CHECK_FOR_ENEMY_INTERVAL = authoring.CHECK_FOR_ENEMY_INTERVAL,
                TIME_BETWEEN_BULLETS = authoring.TIME_BETWEEN_BULLETS,
                REST_TIME_AFTER_BULLETS = authoring.REST_TIME_AFTER_BULLETS
            });
            AddComponent(entity, new GraveComponent
            {
                numGravePieces = authoring.GRAVE_PIECES_TO_DROP
            });
            AddComponent(entity, new Health
            {
                Value = authoring.HEALTH
            });
        }
    }
}

public struct ShooterGraveComponent : IComponentData 
{

    public float PLAYER_CHECK_FOR_ENEMY_RADIUS;
    public float SHOOTING_RADIUS;
    public int BULLETS_TO_SHOOT;
    public float CHECK_FOR_ENEMY_INTERVAL;
    public float TIME_BETWEEN_BULLETS;
    public float REST_TIME_AFTER_BULLETS;

    public float timer;
    public bool TIMER_foundNoTarget;
    public int bulletsShot;
    public bool TIMER_isShooting;

    public float enemies;
    public float playerdist;

    public bool triggerShoot;
    public float3 targetPos;
}