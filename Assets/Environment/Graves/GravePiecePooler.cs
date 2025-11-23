using System.Collections.Generic;
using UnityEngine;

public class GravePiecePooler : MonoBehaviour
{
    List<GameObject> GravePiecePool;
    [SerializeField] int PrespawnGravePieceAmount;
    [SerializeField] GameObject piecePrefab;
    private void Start()
    {
        GravePiecePool = new();

        for (int i = 0; i < PrespawnGravePieceAmount; i++)
        {
            GravePiece piece = Instantiate(piecePrefab).GetComponent<GravePiece>();
            piece.gameObject.SetActive(false);
            GravePiecePool.Add(piece.gameObject);
        }
    }

    public GravePiece GetGravePiece()
    {
        for (int i = 0; i < GravePiecePool.Count; i++)
        {
            if (!GravePiecePool[i].gameObject.activeInHierarchy)
            {
                return GravePiecePool[i].GetComponent<GravePiece>();
            }
        }

        GravePiece piece = Instantiate(piecePrefab).GetComponent<GravePiece>();
        piece.gameObject.SetActive(false);
        GravePiecePool.Add(piece.gameObject);
        return piece;
    }
}
