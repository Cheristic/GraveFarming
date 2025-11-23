using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.InputSystem;

public class BreakGrave : MonoBehaviour
{
    Vector2 breakPos;
    [SerializeField] GraveObjectPooler gravePooler;
    [SerializeField] GravePiecePooler piecePooler;
    float cellSize { get => 1.0f / GridManager.Instance.WorldtoGridRatio; } 
    [SerializeField] int numGravePieces;

    private void Start()
    {
        PlayerManager.Instance.Input.Player.BreakGrave.started += AttemptBreakGrave;
    }
    private void Update()
    {
        
    }

    void AttemptBreakGrave(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GridManager.Instance.HasGraveAt(mousePos))
        {
            Vector2Int gridIndex = GridManager.Instance.ToGridSpace(mousePos);
            breakPos = GridManager.Instance.ToWorldSpace(gridIndex);
            (Grave grave, bool graveThere) breakGrave = gravePooler.GraveAt(breakPos);
            if (breakGrave.graveThere)
            {
                Grave grave = breakGrave.grave;
                grave.gameObject.GetComponent<Grave>().Break(gridIndex);
                breakPos = GridManager.Instance.ToWorldSpace(gridIndex);
                breakPos += new Vector2(cellSize / 2.0f - 1.0f, cellSize / 2.0f - 1.0f);
                dropPieces(breakPos);
            }

        }
    }

    public void dropPieces(Vector2 pos)
    {   
        for (int i = 0;  i < numGravePieces; i++)
        {
            Vector2 piecePos = RandomDropPos(pos, new Vector2(2 * cellSize / 3, 2 * cellSize / 3));
            GravePiece piece = piecePooler.GetGravePiece();
            piece.gameObject.transform.position = piecePos;
            piece.Spawn();
        }
    }

    Vector2 RandomDropPos(Vector2 pos, Vector2 range)
    {
        System.Random rand = new System.Random();
        Vector2 randomMod = new Vector2((float)(rand.NextDouble() - 0.5) * range.x, (float)(rand.NextDouble() - 0.5) * range.y);
        Vector2 piecePos = pos + randomMod;
        return piecePos;
    }
}

