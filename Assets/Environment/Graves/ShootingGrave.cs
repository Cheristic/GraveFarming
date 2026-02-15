using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ShooterGrave : Grave
{
    [SerializeField] int BULLETS_TO_POOL = 5;
    [SerializeField] GameObject _SHOOTER_BULLET;
    [SerializeField] float PLAYER_CHECK_FOR_ENEMY_RADIUS;
    [SerializeField] float SHOOTING_RADIUS;
    [SerializeField] int BULLETS_TO_SHOOT = 3;
    [SerializeField] float CHECK_FOR_ENEMY_INTERVAL = .5f;
    [SerializeField] float TIME_BETWEEN_BULLETS = .2f;
    [SerializeField] float REST_TIME_AFTER_BULLETS = 1f;

    List<ShooterBullet> BulletPool;
    public override void Init()
    {
        base.Init();
        BulletPool = new List<ShooterBullet>();
        for (int i = 0; i < BULLETS_TO_POOL; i++)
        {
            AddBulletToPool();
        }
    }

    ShooterBullet AddBulletToPool()
    {
        ShooterBullet bul = Instantiate(_SHOOTER_BULLET, transform).GetComponent<ShooterBullet>();
        bul.gameObject.SetActive(false);
        BulletPool.Add(bul);
        return bul;
    }

    ShooterBullet FetchBulletFromPool()
    {
        for (int i = 0; i < BulletPool.Count; i++)
        {
            if (!BulletPool[i].gameObject.activeInHierarchy)
            {
                return BulletPool[i];
            }
        }
        return AddBulletToPool();
    }

    public override void Spawn(Vector2 location)
    {
        base.Spawn(location);
        StartCoroutine(ShooterBehaviorLoop());
    }

    IEnumerator ShooterBehaviorLoop()
    {
        while (true)
        {
            Collider2D target = null;
            Vector2 playerPos = PlayerManager.Instance.transform.position;
            Collider2D[] near_player = Physics2D.OverlapCircleAll(playerPos, PLAYER_CHECK_FOR_ENEMY_RADIUS, GlobalLayers.GetLayerMask(GlobalLayers.Layers.Enemies));
            if (near_player.Length > 0) // there are enemies around player
            {
                Collider2D closestHit = null;
                float closestDistance = Mathf.Infinity;
                foreach (var hit in near_player)
                {
                    float dist_to_player = Vector2.Distance(playerPos, hit.transform.position);
                    float dist_to_grave = Vector2.Distance(transform.position, hit.transform.position);
                    if (dist_to_player < closestDistance && dist_to_grave < SHOOTING_RADIUS)
                    {
                        closestHit = hit;
                        closestDistance = dist_to_player;
                    }
                }
                if (closestHit != null) target = closestHit;
            } 
            if (target == null) // either no enemies near player or none within range
            {
                Collider2D[] near_grave = Physics2D.OverlapCircleAll(transform.position, SHOOTING_RADIUS, GlobalLayers.GetLayerMask(GlobalLayers.Layers.Enemies));
                Collider2D closestHit = null;
                float closestDistance = Mathf.Infinity;
                foreach (var hit in near_grave)
                {
                    float dist = Vector2.Distance(transform.position, hit.transform.position);
                    if (dist < closestDistance)
                    {
                        closestHit = hit;
                        closestDistance = dist;
                    }
                }
                if (closestHit != null) target = closestHit;
            }

            if (target == null) // No target within range
            {
                yield return new WaitForSeconds(CHECK_FOR_ENEMY_INTERVAL);
                continue; 
            }

            // SHOOT
            for (int i = 0; i < BULLETS_TO_SHOOT; i++) 
            {
                if (target == null || !target.gameObject.activeInHierarchy) break;
                var bullet = FetchBulletFromPool();
                bullet.Shoot(gameObject, gameObject.transform.position, target.transform.position);

                if (i == BULLETS_TO_SHOOT - 1) yield return new WaitForSeconds(REST_TIME_AFTER_BULLETS);
                else yield return new WaitForSeconds(TIME_BETWEEN_BULLETS);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Color col = Gizmos.color;
        col = new Color(col.r, col.g, col.b, .2f);
        Gizmos.color = col;
        Gizmos.DrawSphere((Vector2)PlayerManager.Instance.transform.position, PLAYER_CHECK_FOR_ENEMY_RADIUS);
        Gizmos.color = Color.yellow;
        col = new Color(col.r, col.g, col.b, .2f);
        Gizmos.color = col;

        Gizmos.DrawSphere((Vector2)transform.position, SHOOTING_RADIUS);
    }
}
