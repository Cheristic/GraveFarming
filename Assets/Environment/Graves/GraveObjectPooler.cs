using UnityEngine;
using System.Collections.Generic;


public class GraveObjectPooler : MonoBehaviour
{
    List<List<GameObject>> GravePools;
    [SerializeField] int PrespawnGraveAmount;

    private void OnEnable()
    {
        GravePools = new();

        for (int i = 0; i < GraveDatabase.Instance.GraveList.Count; i++) // Currently, GraveList is empty, and thus this returns an error
        {
            List<GameObject> GravePool = new();

            for (int j = 0; j < PrespawnGraveAmount; j++)
            {
                Grave grave = Instantiate(GraveDatabase.Instance.GraveList[i].gravePrefab).GetComponent<Grave>();
                grave.gameObject.SetActive(false);
                GravePool.Add(grave.gameObject); // Added .gameObject
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

        Grave grave = Instantiate(GraveDatabase.Instance.GraveList[(int)type].gravePrefab).GetComponent<Grave>();
        grave.gameObject.SetActive(false);
        pool.Add(grave.gameObject);
        return grave;
    }
}
