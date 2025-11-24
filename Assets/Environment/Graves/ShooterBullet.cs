using System.Collections;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float MOVE_SPEED;
    [SerializeField] LayerMask HittableLayers;
    [SerializeField] LayerMask EnemyLayer;
    [SerializeField] float TIME_OUT = 5f;

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

        if (((1 << collision.gameObject.layer) & HittableLayers.value) != 0)
        {
            if (((1 << collision.gameObject.layer) & EnemyLayer.value) != 0)
            {
                collision.gameObject.GetComponent<IHittable>().Hit();
            }
            gameObject.SetActive(false);
        }
    }
}
