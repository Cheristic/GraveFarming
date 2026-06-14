using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    public GraveObjectPooler gravePooler;
    public GravePiecePooler gravePiecePooler;
    public SoulPiecePooler soulPiecePooler;

    public EnemyPooler enemyPooler;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}