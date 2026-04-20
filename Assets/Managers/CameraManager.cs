using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Camera mainCam;
    [SerializeField] Camera fixedCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            mainCam.gameObject.SetActive(false);
            fixedCam.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            mainCam.gameObject.SetActive(true);
            fixedCam.gameObject.SetActive(false);
        }
    }
}
