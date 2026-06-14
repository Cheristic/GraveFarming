using System;
using UnityEngine;
public class PlayerHealth : Entity
{
    [SerializeField] float invulnerabilityTime = .33f;
    private new void Awake()
    {
        base.Awake();
    }

    float lastTimeHit = 0;
    public override void Hit(float dmg)
    {
        if ((Time.time - lastTimeHit) < invulnerabilityTime) return;
        base.Hit(dmg);
        lastTimeHit = Time.time;
    }

    public override void Die()
    {
        GameManager.Main.EndGame();
        gameObject.SetActive(false);
    }
}