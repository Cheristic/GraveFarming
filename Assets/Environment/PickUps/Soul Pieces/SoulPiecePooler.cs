using System.Collections.Generic;
using UnityEngine;

public class SoulPiecePooler : MonoBehaviour
{
    List<GameObject> SoulPiecePool;
    [SerializeField] int PrespawnSoulPieceAmount;
    [SerializeField] GameObject soulPiecePrefab;
    private void Start()
    {
        SoulPiecePool = new();

        for (int i = 0; i < PrespawnSoulPieceAmount; i++)
        {
            SoulPiece piece = Instantiate(soulPiecePrefab, this.transform).GetComponent<SoulPiece>();
            piece.gameObject.SetActive(false);
            SoulPiecePool.Add(piece.gameObject);
        }
    }

    public SoulPiece GetSoulPiece()
    {
        for (int i = 0; i < SoulPiecePool.Count; i++)
        {
            if (!SoulPiecePool[i].gameObject.activeInHierarchy)
            {
                return SoulPiecePool[i].GetComponent<SoulPiece>();
            }
        }

        SoulPiece piece = Instantiate(soulPiecePrefab, this.transform).GetComponent<SoulPiece>();
        piece.gameObject.SetActive(false);
        SoulPiecePool.Add(piece.gameObject);
        return piece;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SoulPiece soul = GetSoulPiece();
            soul.Spawn();
            soul.gameObject.transform.position = PlayerManager.Instance.gameObject.transform.position + new Vector3(1.0f, 1.0f, 0.0f);
        }
    }
}
