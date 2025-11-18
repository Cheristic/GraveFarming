using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GraveDatabase.ResourceRequirements[] playerResources;

    public static PlayerManager Instance { get; private set; }
    [SerializeField] PlayerControls controls;
    public PlayerInput Input => controls.input;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        controls.Init();
    }

    public static int CompareResourceRequirements(GraveDatabase.ResourceRequirements req1, GraveDatabase.ResourceRequirements req2)
    {
        if ((int)req1.type <= (int)req2.type) return -1;
        else if ((int)req1.type >= (int)req2.type) return 1;

        return 0;
    }

    private void Start()
    {
        Array.Sort(playerResources, CompareResourceRequirements);
    }
}
