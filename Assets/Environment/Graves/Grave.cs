using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour
{
    public Vector2Int GridSpot;

    public void Spawn(Vector2 location) 
    {
        gameObject.SetActive(true);
        transform.position = location;
    }

    public void Break() 
    {
    //    Grid.Remove(GridIndex)
    //  gameObject.SetActive(false);
    }
}


