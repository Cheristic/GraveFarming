using System.Collections.Generic;
using UnityEngine;

public class BreakGrave : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Space) && GridManager.Instance.HasGraveAt(mousePos)) 
        {
            this.gameObject.GetComponent<Grave>().Break(mousePos);
        }
    }
}

