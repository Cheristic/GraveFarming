using UnityEngine;

public class Entity : MonoBehaviour, IHittable
{
    [SerializeField] float MAX_HEALTH;
    internal float _currentHealth;
    protected Rigidbody2D _rb;

    public void Awake()
    {
        _currentHealth = MAX_HEALTH;
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Hit(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0f) Die();
        else Debug.Log($"{name} got hit! HP: {_currentHealth}/{MAX_HEALTH}");
    }

    public virtual void Die()
    {
        Debug.Log($"{name} died.");
        Destroy(gameObject);
    }
}