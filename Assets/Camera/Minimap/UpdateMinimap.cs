using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;

public class UpdateMinimap : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mapPlayer;
    RectTransform mapRectTransform;
    Vector3 rectProportions;
    Vector2 gridWorldSize { get => GridManager.Instance.ToWorldSpace(GridManager.Instance.GridDimensions); }

    [SerializeField] MinimapObjectPooler pooler;

    void Awake()
    {
        mapRectTransform = GameObject.Find("Map").GetComponent<RectTransform>();
        rectProportions = new Vector3(mapRectTransform.rect.size.x, mapRectTransform.rect.size.y, 1.0f);

        GridManager.GravePlaced += PlaceGrave;
        GridManager.GraveDestroyed += DestroyGrave;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerMinimapPos2D = WorldToMinimapPos(player.transform.position);
        mapPlayer.transform.localPosition = new Vector3(playerMinimapPos2D.x, playerMinimapPos2D.y, mapPlayer.transform.position.z);
    }

    private Vector2 GridToMinimapPos(Vector2Int gridPos)
    {
        Vector2 gridWorldPos = GridManager.Instance.ToWorldSpace(gridPos);
        return WorldToMinimapPos(gridWorldPos);
    }
    private Vector2 WorldToMinimapPos(Vector2 worldPos)
    {
        Vector2 targetPos = Vector2.Scale(worldPos + new Vector2(1.0f, 1.0f), new Vector2(rectProportions.x, rectProportions.y));
        return new Vector2(targetPos.x / gridWorldSize.x, targetPos.y / gridWorldSize.y);
    }

    private Vector2 MinimapToWorldPos(Vector2 pos)
    {
        Vector2 worldPos = Vector2.Scale(pos, gridWorldSize);
        worldPos = new Vector2(worldPos.x / rectProportions.x, worldPos.y / rectProportions.y) - new Vector2(1.0f, 1.0f);
        return worldPos;
    }

    private void PlaceGrave(Vector2Int gridPos)
    {
        //MinimapGrave grave = pooler.GetMinimapGrave();
        //grave.Spawn(GridToMinimapPos(gridPos));
    }

    private void DestroyGrave(Vector2Int gridPos)
    {
        //Vector2 pos = GridToMinimapPos(gridPos);
        //foreach (GameObject grave in pooler.ObjectPool)
        //{
        //    if (grave.transform.localPosition == new Vector3(pos.x, pos.y, grave.transform.position.z))
        //    {
        //        grave.GetComponent<MinimapObject>().Break();
        //        return;
        //    }
        //}
    }

    float cellSize { get => 1.0f / GridManager.Instance.WorldtoGridRatio; }
    //private bool IsPositionWithinCell(Vector2 pos, Vector2Int gridPos)
    //{

    //}
}
