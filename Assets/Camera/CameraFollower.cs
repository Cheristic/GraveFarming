using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 DEADZONE_SIZE;

    // Cameras and camera bounds
    Vector2 boundsX;
    Vector2 boundsY;

    private void OnEnable()
    {
        Vector2 worldMax = GridManager.Instance.ToWorldSpace(GridManager.Instance.GridDimensions) - new Vector2(1.0f, 1.0f);

        boundsX = new Vector2(worldMax.x / 2 - DEADZONE_SIZE.x / 2, worldMax.x / 2 + DEADZONE_SIZE.x / 2);
        boundsY = new Vector2(worldMax.y / 2 - DEADZONE_SIZE.y / 2, worldMax.y / 2 + DEADZONE_SIZE.y / 2);

        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

       //// Create bounds relative to camera center
       //boundsX = new Vector2(cam.aspect * cam.orthographicSize - 1, worldMax.x - cam.aspect * cam.orthographicSize);
       // boundsY = new Vector2(cam.orthographicSize - 1, worldMax.y - cam.orthographicSize);

       // Vector3 targetPos = player.position;
       // targetPos.z = transform.position.z;

       // if (targetPos.x <= boundsX.x) targetPos.x = boundsX.x;
       // else if (targetPos.x >= boundsX.y) targetPos.x = boundsX.y;

       // if (targetPos.y <= boundsY.x) targetPos.y = boundsY.x;
       // else if (targetPos.y >= boundsY.y) targetPos.y = boundsY.y;

       // transform.position = targetPos;
    }
    void Update()
    {
        // #### ROTATION ####
        //Vector2 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(player.position);

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 targetPos = dir.normalized * (Vector2.Distance(mousePos, player.position) / distanceLimiter);
        //Vector3 targetPos = player.position;// Vector3.ClampMagnitude(targetPos, threshold) + player.position;
        //targetPos.z = transform.position.z;

        transform.position = new Vector3(Mathf.Clamp(player.position.x, boundsX.x, boundsX.y),
                                        Mathf.Clamp(player.position.y, boundsY.x, boundsY.y),
                                        transform.position.z);

        //if (targetPos.x <= boundsX.x || targetPos.x >= boundsX.y)
        //{
        //    targetPos.x = transform.position.x;
        //}

        //if (targetPos.y <= boundsY.x || targetPos.y >= boundsY.y)
        //{
        //    targetPos.y = transform.position.y;
        //}

        //Debug.Log(targetPos);
        //transform.position = targetPos;
    }
}
