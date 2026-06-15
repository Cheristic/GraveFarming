using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerformanceTestManager : MonoBehaviour
{
    [SerializeField] GameObject[] DisableTheseObjects;
    private void Awake()
    {
        UnityEngine.Random.InitState(0);
    }

    private void Start()
    {
        PlayerManager.Instance.transform.position = GridManager.Instance.ToWorldSpace(new Vector2Int(GridManager.Instance.GridDimensions.x / 2, GridManager.Instance.GridDimensions.y / 2));
        PlaceStartingGraves();
        foreach (var obj in DisableTheseObjects) obj.SetActive(false);
    }

    void PlaceStartingGraves()
    {
        for (int x = 0; x < GridManager.Instance.GridDimensions.x; x++)
            for (int y = 0; y < GridManager.Instance.GridDimensions.y; y++)
            {
                if (x == GridManager.Instance.GridDimensions.x / 2 && y == GridManager.Instance.GridDimensions.y / 2) continue;

                Vector2 graveLocation = GridManager.Instance.PlaceGrave(GridManager.Instance.ToWorldSpace(new Vector2Int(x, y)));
                Grave grave = PoolManager.Instance.gravePooler.GetGrave(GraveDatabase.Instance.GraveList[0].type);
                grave.Spawn(graveLocation);
            }
    }
}
