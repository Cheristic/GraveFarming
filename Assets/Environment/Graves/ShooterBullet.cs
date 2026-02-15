using System.Collections;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float MOVE_SPEED;
    [SerializeField] float TIME_OUT = 5f;
    [SerializeField] float DAMAGE = 5f;

    GameObject _firingSource;
    public void Shoot(GameObject source, Vector3 sourcePos, Vector3 target)
    {
        _firingSource = source;
        gameObject.SetActive(true);
        transform.position = sourcePos;
        Vector2 dir = target - transform.position;
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        rb.linearVelocity = dir * MOVE_SPEED;
        StartCoroutine(TimeOut());
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(TIME_OUT);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _firingSource) { return; }

        if (GlobalLayers.IsOnLayer(collision.gameObject, GlobalLayers.Layers.Hittable))
        {
            if (collision.gameObject.TryGetComponent<IHittable>(out var hit)) hit.Hit(DAMAGE);
            gameObject.SetActive(false);
        }
    }
}
