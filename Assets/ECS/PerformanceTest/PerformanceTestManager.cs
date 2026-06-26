using NUnit.Framework.Internal;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerformanceTestManager : MonoBehaviour
{
    public static PerformanceTestManager Instance;
    [SerializeField] GameObject[] DisableTheseObjects;

    private void Awake()
    {
        Instance = this;
        UnityEngine.Random.InitState(0);
        RoundManager.TriggerActivePhase += () => StartCoroutine(TrackData());
    }

    private void Start()
    {
        PlayerManager.Instance.transform.position = GridManager.Instance.ToWorldSpace(new Vector2Int(GridManager.Instance.GridDimensions.x / 2, GridManager.Instance.GridDimensions.y / 2));
        //PlaceStartingGraves();
        foreach (var obj in DisableTheseObjects) obj.SetActive(false);
    }

    IEnumerator TrackData()
    {
        int frame = 0;
        float time = 0;
        while (frame < 150)
        {
            yield return null;
            frame++;
            time += Time.deltaTime;
            Debug.Log(frame);
        }

        Debug.Log(time / frame);
    }
}
