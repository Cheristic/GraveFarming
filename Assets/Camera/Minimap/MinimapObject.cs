using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapObject : MonoBehaviour
{ 
    public void Spawn(Vector2 pos)
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(pos.x, pos.y, transform.position.z);
        Debug.Log(transform.localPosition);
    }
    public void Spawn(Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.localPosition = pos;
    }
    public void Break()
    {
        gameObject.SetActive(false);
    }
}
