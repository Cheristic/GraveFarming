using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaceGraves : MonoBehaviour
{
    [SerializeField] GraveObjectPooler pooler;
    [SerializeField] SpriteRenderer GravePreviewLocation;
    int soulIndex = (int)GraveDatabase.Resources.SoulPieces;
    int graveIndex = (int)GraveDatabase.Resources.GravePieces;

    GraveDatabase.GraveData LastSelectedType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LastSelectedType = GraveDatabase.Instance.GraveList[0];
        PlayerManager.Instance.Input.Player.PlaceGrave.started += AttemptPlaceGrave;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LastSelectedType = GraveDatabase.Instance.GetGraveData(GraveDatabase.GraveType.Shooter);
        } else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LastSelectedType = GraveDatabase.Instance.GetGraveData(GraveDatabase.GraveType.Blocker);
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!GridManager.Instance.HasGraveAt(mousePos) && GridManager.Instance.ToGridSpace(mousePos) != GridManager.Instance.ToGridSpace(PlayerManager.Instance.transform.position))
        {
            GravePreviewLocation.transform.position = GridManager.Instance.ToWorldSpace(GridManager.Instance.ToGridSpace(mousePos));
            GravePreviewLocation.sprite = LastSelectedType.sprite;
        } else
        {
            GravePreviewLocation.sprite = null;
        }
    }

    void AttemptPlaceGrave(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!GridManager.Instance.HasGraveAt(mousePos) && HasEnoughResources())
        {
            Vector2 graveLocation = GridManager.Instance.PlaceGrave(mousePos);
            Grave grave = pooler.GetGrave(LastSelectedType.type);
            grave.Spawn(graveLocation);
            ChargePlayer();

            GravePreviewLocation.sprite = null;
        }
    }

    bool HasEnoughResources()
    {
        return (PlayerManager.Instance.playerResources[soulIndex].cost >= LastSelectedType.resourceRequirements[soulIndex].cost) && (PlayerManager.Instance.playerResources[graveIndex].cost >= LastSelectedType.resourceRequirements[graveIndex].cost);
    }

    void ChargePlayer()
    {
        PlayerManager.Instance.playerResources[soulIndex].cost -= LastSelectedType.resourceRequirements[soulIndex].cost;
        PlayerManager.Instance.playerResources[graveIndex].cost -= LastSelectedType.resourceRequirements[graveIndex].cost;
    }
}
