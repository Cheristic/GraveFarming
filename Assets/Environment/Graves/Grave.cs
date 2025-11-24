using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour
{
    //public Vector2Int GridSpot;
    [SerializeField] BreakGrave breakScript;
    [SerializeField] float initialHealth;
    float health;

    public virtual void Init() { }
    public virtual void Spawn(Vector2 location) 
    {
        gameObject.SetActive(true);
        transform.position = location;
        health = initialHealth;
    }

    public void Break(Vector2Int gridIndex) 
    {
    //    Grid.Remove(GridIndex)
        gameObject.SetActive(false);
        GridManager.Instance.RemoveGrave(gridIndex.x, gridIndex.y);

        //breakScript.
    }
}


