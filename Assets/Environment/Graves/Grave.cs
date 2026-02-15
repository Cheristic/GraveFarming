using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Grave : MonoBehaviour, IHittable
{
    //public Vector2Int GridSpot;
    [SerializeField] int numGravePieces;
    [SerializeField] float initialHealth;
    float health;

    public virtual void Init() { }
    public virtual void Spawn(Vector2 location) 
    {
        gameObject.SetActive(true);
        transform.position = location;
        health = initialHealth;
    }
    public void Hit(float dmg)
    {
        health -= dmg;
        if (health <= 0.0f) Break();
    }

    float cellSize { get => 1.0f / GridManager.Instance.WorldtoGridRatio; }
    public void Break() 
    {
        Vector2 breakPos = transform.position;
        breakPos += new Vector2(cellSize / 2.0f - 1.0f, cellSize / 2.0f - 1.0f);
        DropPieces(breakPos);
        GridManager.Instance.RemoveGrave(transform.position);
        gameObject.SetActive(false);
    }

    public void DropPieces(Vector2 pos)
    {
        for (int i = 0; i < numGravePieces; i++)
        {
            Vector2 piecePos = GridManager.Instance.NewRandomPos(pos, new Vector2(2 * cellSize / 3, 2 * cellSize / 3));
            GravePiece piece = PoolManager.Instance.piecePooler.GetGravePiece();
            piece.gameObject.transform.position = piecePos;
            piece.Spawn();
        }
    }


}


