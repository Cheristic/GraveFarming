using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerControls controls;
    private void Awake()
    {
        controls.Init();
    }
}
