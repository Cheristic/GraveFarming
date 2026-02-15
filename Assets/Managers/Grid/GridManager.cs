using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] public Vector2Int GridDimensions;
    [SerializeField] public float WorldtoGridRatio;

    int[,] Grid;

    public Vector2Int ToGridSpace(Vector2 v) => new(Mathf.RoundToInt(v.x * WorldtoGridRatio), Mathf.RoundToInt(v.y * WorldtoGridRatio));
    public Vector2 ToWorldSpace(Vector2Int v) => new Vector2(v.x / WorldtoGridRatio, v.y / WorldtoGridRatio);
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        Grid = new int[GridDimensions.x, GridDimensions.y];
    }
    public Vector2 PlaceGrave(Vector2 pos)
    {
        Vector2Int gridPos = ToGridSpace(pos);
        Grid[gridPos.x, gridPos.y] = 1;
        return ToWorldSpace(gridPos);
    }

    public void RemoveGrave(Vector2 graveWorldPos)
    {
        Vector2Int gridPos = ToGridSpace(graveWorldPos);
        Grid[gridPos.x, gridPos.y] = 0;
    }

    public bool HasGraveAt(Vector2 pos)
    {
        Vector2Int gridPos = ToGridSpace(pos);
        if (gridPos.x >= GridDimensions.x || gridPos.x < 0 || gridPos.y >= GridDimensions.y || gridPos.y < 0) return true;
        return Grid[gridPos.x, gridPos.y] == 1;
    }

    public Vector2Int RandomGridPos()
    {
        System.Random rand = new System.Random();
        return new Vector2Int(rand.Next(GridDimensions.x), rand.Next(GridDimensions.y));
    }

    public Vector2 NewRandomPos(Vector2 pos, Vector2 range)
    {
        System.Random rand = new System.Random();
        Vector2 randomMod = new Vector2((float)(rand.NextDouble() - 0.5) * range.x, (float)(rand.NextDouble() - 0.5) * range.y);
        Vector2 newPos = pos + randomMod;
        return newPos;
    }
}
