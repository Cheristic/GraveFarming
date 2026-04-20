using UnityEngine;

public class FixedCameraSetter : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = gameObject.GetComponent<Camera>();
        Vector2 dims = GridManager.Instance.ToWorldSpace(GridManager.Instance.GridDimensions);
        transform.position = new Vector3(dims.x, dims.y, transform.position.z) / 2.0f - new Vector3(1.0f, 1.0f, 0.0f);
        cam.orthographicSize = dims.y / 2.0f;
        cam.aspect = dims.x / (2.0f * cam.orthographicSize);
    }
}
