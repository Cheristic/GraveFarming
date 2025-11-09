using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour
{
    public int GridIndex;

    public void Spawn() 
    {
        gameObject.SetActive(true);
    }

    public void Break() 
    {
    //    Grid.Remove(GridIndex)
    }
}


