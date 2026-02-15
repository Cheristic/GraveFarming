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
    float cellSize { get => 1.0f / GridManager.Instance.WorldtoGridRatio; }
    public void Hit(float dmg)
    {
        health -= dmg;
        if (health <= 0.0f) Break();
    }
    public void Break() 
    {
        Vector2 breakPos = transform.position;
        breakPos += new Vector2(cellSize / 2.0f - 1.0f, cellSize / 2.0f - 1.0f);
        dropPieces(breakPos);
        GridManager.Instance.RemoveGrave(transform.position);
        gameObject.SetActive(false);
    }

    public void dropPieces(Vector2 pos)
    {
        Vector2 RandomDropPos(Vector2 pos, Vector2 range)
        {
            System.Random rand = new System.Random();
            Vector2 randomMod = new Vector2((float)(rand.NextDouble() - 0.5) * range.x, (float)(rand.NextDouble() - 0.5) * range.y);
            Vector2 piecePos = pos + randomMod;
            return piecePos;
        }

        for (int i = 0; i < numGravePieces; i++)
        {
            Vector2 piecePos = RandomDropPos(pos, new Vector2(2 * cellSize / 3, 2 * cellSize / 3));
            GravePiece piece = PoolManager.Instance.piecePooler.GetGravePiece();
            piece.gameObject.transform.position = piecePos;
            piece.Spawn();
        }
    }


}


