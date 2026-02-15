using UnityEngine;
using System.Collections.Generic;


public class GraveObjectPooler : MonoBehaviour
{
    List<List<GameObject>> GravePools;
    [SerializeField] int PrespawnGraveAmount;

    private void Start()
    {
        GravePools = new();

        for (int i = 0; i < GraveDatabase.Instance.GraveList.Count; i++)
        {
            List<GameObject> GravePool = new();

            for (int j = 0; j < PrespawnGraveAmount; j++)
            {
                Grave grave = Instantiate(GraveDatabase.Instance.GraveList[i].gravePrefab, this.transform).GetComponent<Grave>();
                grave.gameObject.SetActive(false);
                grave.Init();
                GravePool.Add(grave.gameObject);
            }

            GravePools.Add(GravePool);
        }
    }

    public Grave GetGrave(GraveDatabase.GraveType type)
    {
        List<GameObject> pool = GravePools[(int)type];

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
        pool.Add(grave.gameObject);
        return grave;
    }
}
