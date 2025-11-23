using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GraveDatabase.ResourceRequirements[] playerResources;
    [SerializeField] TextMeshProUGUI graveValue;
    [SerializeField] TextMeshProUGUI soulValue;

    public static PlayerManager Instance { get; private set; }
    [SerializeField] PlayerControls controls;
    public BoxCollider2D _collider;
    public PlayerInput Input => controls.input;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        controls.Init();
    }

    public static int CompareResourceRequirements(GraveDatabase.ResourceRequirements req1, GraveDatabase.ResourceRequirements req2)
    {
        if ((int)req1.type < (int)req2.type) return -1;
        else if ((int)req1.type > (int)req2.type) return 1;

        return 0;
    }

    private void Start()
    {
        Array.Sort(playerResources, GraveDatabase.CompareResourceRequirements);
        soulValue.text = playerResources[0].cost.ToString();
        graveValue.text = playerResources[1].cost.ToString();

    }

    public void ModifyResource(GraveDatabase.Resources type, int amount)
    {
        playerResources[(int)type].cost += amount;
        switch ((int)type)
        {
            case 0:
                soulValue.text = playerResources[0].cost.ToString();
                break;

            case 1:
                graveValue.text = playerResources[1].cost.ToString();
                break;

            default:
                return;
        }
    }
}
