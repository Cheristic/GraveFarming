using UnityEngine;

public class Enemy2 : MonoBehaviour, IHittable
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float contactDamage = 10f;
    [SerializeField] private float attackCooldown = 2.25f;

    private float _currentHealth;
    private float _lastAttackTime = float.MinValue;
    private Transform _target;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _currentHealth = maxHealth;
        _rb = GetComponent<Rigidbody2D>();
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

    public void Hit(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0f) Die();
        else Debug.Log($"{name} got hit! HP: {_currentHealth}/{maxHealth}");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        TryAttack(collision.collider.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        TryAttack(other.gameObject);
    }

    private void TryAttack(GameObject player)
    {
        if (!CanAttack()) return;

        _lastAttackTime = Time.time;
        player.SendMessage("TakeDamage", contactDamage, SendMessageOptions.DontRequireReceiver);
    }

    private bool CanAttack()
    {
        return Time.time - _lastAttackTime >= attackCooldown;
    }

    private void AcquireTarget()
    {
        _target = PlayerManager.Instance != null ? PlayerManager.Instance.transform : null;
    }

    private void Die()
    {
        Debug.Log($"{name} died.");
        Destroy(gameObject);
    }
}
