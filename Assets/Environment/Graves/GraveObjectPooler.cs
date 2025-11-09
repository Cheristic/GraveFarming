using UnityEngine;
using System.Collections.Generic;


public class GraveObjectPooler : MonoBehaviour
{
    List<List<GameObject>> GravePools;
    [SerializeField] int PrespawnGraveAmount;

    private void OnEnable()
    {
        GravePools = new();

        for (int i = 0; i < GraveDataBase.Instance.GraveList.Count; i++)
        {
            List<GameObject> GravePool = new();

            for (int j = 0; j < PrespawnGraveAmount; j++)
            {
                Grave grave = Instantiate(GraveDataBase.Instance.GraveList[i].gravePrefab).GetComponent<Grave>();
                grave.gameObject.SetActive(false);
                GravePool.Add(grave);
            }

            GravePools.Add(GravePool);
        }
    }

    public Grave GetGrave(GraveDataBase.GraveType type)
    {
        List<GameObject> pool = GravePools[(int)type];

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {

                return pool[i];
            }
        }

        Grave grave = Instantiate(GraveDataBase.Instance.GraveList[(int)type].gravePrefab).GetComponent<Grave>();
        grave.gameObject.SetActive(false);
        pool.Add(grave);
        return grave;
    }
}
