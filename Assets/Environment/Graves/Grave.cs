using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour
{
    //public Vector2Int GridSpot;
    [SerializeField] BreakGrave breakScript;

    public void Spawn(Vector2 location) 
    {
        gameObject.SetActive(true);
        transform.position = location;
    }

    public void Break(Vector2Int gridIndex) 
    {
    //    Grid.Remove(GridIndex)
        gameObject.SetActive(false);
        GridManager.Instance.RemoveGrave(gridIndex.x, gridIndex.y);

        //breakScript.
    }
}


