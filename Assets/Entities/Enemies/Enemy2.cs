using UnityEngine;
using Layers = GlobalLayers.Layers;

public class Enemy2 : Entity
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float contactDamage = 10f;
    [SerializeField] private float attackCooldown = 2.25f;

    private float _lastAttackTime = float.MinValue;
    private Transform _target;

    private new void Awake()
    {
        base.Awake();
        if (_rb != null) _rb.bodyType = RigidbodyType2D.Dynamic;
        AcquireTarget();
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            AcquireTarget();
            return;
        }

        Vector2 toTarget = (Vector2)_target.position - _rb.position;
        float distance = toTarget.magnitude;
        if (distance <= 0.05f) return;

        Vector2 direction = toTarget / distance;
        _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!CanAttack()) return;

        if (GlobalLayers.IsOnLayer(collision.gameObject, Layers.Player) ||
            GlobalLayers.IsOnLayer(collision.gameObject, Layers.Grave))
            if (collision.gameObject.TryGetComponent<IHittable>(out var hit))
            {
                hit.Hit(contactDamage);
                _lastAttackTime = Time.time;
            }
    }

    private bool CanAttack()
    {
        return Time.time - _lastAttackTime >= attackCooldown;
    }

    private void AcquireTarget()
    {
        _target = PlayerManager.Instance != null ? PlayerManager.Instance.transform : null;
    }
}
