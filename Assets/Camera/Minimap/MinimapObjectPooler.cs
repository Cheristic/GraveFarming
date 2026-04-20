using System.Collections.Generic;
using UnityEngine;

public class MinimapObjectPooler : MonoBehaviour
{
    public List<GameObject> ObjectPool;
    [SerializeField] int PrespawnObjectsAmount;
    [SerializeField] GameObject minimapGravePrefab;
    private void Start()
    {
        ObjectPool = new();

        for (int j = 0; j < PrespawnObjectsAmount; j++)
        {
            MinimapGrave grave = Instantiate(minimapGravePrefab, this.transform).GetComponent<MinimapGrave>();
            grave.gameObject.SetActive(false);
            ObjectPool.Add(grave.gameObject);
        }        
    }

    public MinimapGrave GetMinimapGrave()
    {
        for (int i = 0; i < ObjectPool.Count; i++)
        {
            if (!ObjectPool[i].gameObject.activeInHierarchy)
            {
                return ObjectPool[i].gameObject.GetComponent<MinimapGrave>();
            }
        }

        MinimapGrave grave = Instantiate(minimapGravePrefab, this.transform).GetComponent<MinimapGrave>();
        grave.gameObject.SetActive(false);
        ObjectPool.Add(grave.gameObject);
        return grave;
    }
}
