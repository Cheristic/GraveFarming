using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BreakGrave : MonoBehaviour
{
    Vector2 breakPos;
    float cellSize = 1.0f / GridManager.Instance.WorldtoGridRatio;
    [SerializeField] int numGravePieces;
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Space) && GridManager.Instance.HasGraveAt(mousePos)) 
        {
            Vector2Int gridIndex = GridManager.Instance.ToGridSpace(mousePos);
            this.gameObject.GetComponent<Grave>().Break(gridIndex);
            breakPos = GridManager.Instance.ToWorldSpace(gridIndex);
            breakPos += new Vector2(cellSize / 2.0f, cellSize / 2.0f);
            dropPieces(breakPos);
        }
    }

    public void dropPieces(Vector2 pos)
    {   
        for (int i = 0;  i < numGravePieces; i++)
        {
            RandomDropPos(pos);
        }
    }

    Vector2 RandomDropPos(Vector2 range)
    {
        // NOT USE GridDomensions, but find cell dim
        // Finish this
        return new Vector2();
    }
}

