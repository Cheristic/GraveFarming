using UnityEngine;

public class PlayerPlaceGraves : MonoBehaviour
{
    [SerializeField] GraveObjectPooler pooler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Grave grave = pooler.GetGrave(0);
            Grave newGrave = Instantiate(grave);
            newGrave.transform.position = this.transform.position;
            newGrave.Spawn();
        }
    }
}
