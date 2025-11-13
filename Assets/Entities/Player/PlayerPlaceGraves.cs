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
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Grave grave = pooler.GetGrave(GraveDatabase.GraveType.Shooter);
            grave.transform.position = gameObject.transform.position;
            grave.Spawn();
        } else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Grave grave = pooler.GetGrave(GraveDatabase.GraveType.Blocker);
            grave.transform.position = gameObject.transform.position;
            grave.Spawn();
        }
    }
}
