using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;


public class GraveObjectPooler : MonoBehaviour
{
    List<List<Grave>> GravePools;
    [SerializeField] int PrespawnGraveAmount;

    internal List<Grave> ShooterGravePool { get => GravePools[0]; }

    private void Start()
    {
        GravePools = new();

        for (int i = 0; i < GraveDatabase.Instance.GraveList.Count; i++)
        {
            List<Grave> GravePool = new();

            for (int j = 0; j < PrespawnGraveAmount; j++)
            {
                Grave grave = Instantiate(GraveDatabase.Instance.GraveList[i].gravePrefab, this.transform).GetComponent<Grave>();
                grave.gameObject.SetActive(false);
                grave.Init();
                GravePool.Add(grave);
            }

            GravePools.Add(GravePool);
        }
    }

    public Grave GetGrave(GraveDatabase.GraveType type)
    {
        List<Grave> pool = GravePools[(int)type];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i].gameObject.GetComponent<Grave>();
            }
        }

        Grave grave = Instantiate(GraveDatabase.Instance.GraveList[(int)type].gravePrefab, this.transform).GetComponent<Grave>();
        grave.gameObject.SetActive(false);
        grave.Init();
        pool.Add(grave);
        return grave;
    }
}
