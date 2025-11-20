using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour
{
    //public Vector2Int GridSpot;

    public void Spawn(Vector2 location) 
    {
        gameObject.SetActive(true);
        transform.position = location;
    }

    public void Break(Vector2 pos) 
    {
    //    Grid.Remove(GridIndex)
        gameObject.SetActive(false);
        if (GridManager.Instance.HasGraveAt(pos))
        {
            Vector2Int gridIndex = GridManager.Instance.ToGridSpace(pos);
            GridManager.Instance.RemoveGrave(gridIndex.x, gridIndex.y);
        }
    }
}


