using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    [SerializeField] float distanceLimiter;

    void Update()
    {
        // #### ROTATION ####
        Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.position);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 targetPos = dir.normalized * (Vector2.Distance(mousePos, player.position) / distanceLimiter);
        targetPos = Vector3.ClampMagnitude(targetPos, threshold) + player.position;
        targetPos.z = transform.position.z;

        transform.position = targetPos;
    }
}
