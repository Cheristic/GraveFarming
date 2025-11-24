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
    [SerializeField] LayerMask ENEMIES_LAYER;

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
            Collider2D[] hits;
            Vector3 playerPos;
            while (true) {
                playerPos = PlayerManager.Instance.transform.position;
                hits = Physics2D.OverlapCircleAll(playerPos, PLAYER_CHECK_FOR_ENEMY_RADIUS, ENEMIES_LAYER);
                if (hits.Length > 0) break;
                yield return new WaitForSeconds(CHECK_FOR_ENEMY_INTERVAL);
            }
            Collider2D closestHit = null;
            float closestDistance = Mathf.Infinity;
            foreach (var hit in hits)
            {
                float dist = Vector3.Distance(playerPos, hit.transform.position);
                if (dist < closestDistance && dist < SHOOTING_RADIUS) { 
                    closestHit = hit;  
                    closestDistance = dist;
                }
            }
            if (closestHit == null)
            {
                yield return new WaitForSeconds(CHECK_FOR_ENEMY_INTERVAL);
                continue; // All outside shooting radius
            }

            for (int i = 0; i < BULLETS_TO_SHOOT; i++) 
            {
                var bullet = FetchBulletFromPool();
                bullet.Shoot(gameObject, gameObject.transform.position, closestHit.transform.position);

                if (i == BULLETS_TO_SHOOT - 1) yield return new WaitForSeconds(REST_TIME_AFTER_BULLETS);
                else yield return new WaitForSeconds(TIME_BETWEEN_BULLETS);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(PlayerManager.Instance.transform.position, PLAYER_CHECK_FOR_ENEMY_RADIUS);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, SHOOTING_RADIUS);
    }
}
