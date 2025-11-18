using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] Vector2Int GridDimensions;
    [SerializeField] float WorldtoGridRatio;

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

    public void RemoveGrave(int x, int y)
    {
        Grid[x, y] = 0;
    }

    public bool HasGraveAt(Vector2 pos)
    {
        Vector2Int gridPos = ToGridSpace(pos);
        if (gridPos.x >= GridDimensions.x || gridPos.x < 0 || gridPos.y >= GridDimensions.y || gridPos.y < 0) return true;
        return Grid[gridPos.x, gridPos.y] == 1;
    }




}
