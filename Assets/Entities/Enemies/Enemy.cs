using UnityEngine;
public class Enemy : Entity
{
    internal bool hasSpawned = false;
    EnemyDataBase.EnemyData data;
    System.Random rand;
    public void Init(EnemyDataBase.EnemyData data)
    {
        base.Init();
        this.data = data;
        rand = new System.Random(0);
    }
    public override void Spawn(Vector2 location)
    {
        base.Spawn(location);
        hasSpawned = true;
    }
    public override void Die()
    {
        base.Die();
        DropPieces(data.soulPiecesDropped, 2 * GridManager.Instance.cellSize / 3);
    }
    public void DropPieces(int numPieces, float range)
    {
        for (int i = 0; i < numPieces; i++)
        {
            Vector2 piecePos = GridManager.Instance.NewRandomPos(transform.position, new Vector2(range, range), rand);
            SoulPiece piece = PoolManager.Instance.soulPiecePooler.GetSoulPiece();
            piece.gameObject.transform.position = piecePos;
            piece.Spawn();
        }
    }
}