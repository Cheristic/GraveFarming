using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlaceGraves : MonoBehaviour
{
    [SerializeField] GraveObjectPooler pooler;
    [SerializeField] SpriteRenderer GravePreviewLocation;
    int soulIndex = (int)GraveDatabase.Resources.SoulPieces;
    int graveIndex = (int)GraveDatabase.Resources.GravePieces;
    [SerializeField] Color ValidPlaceColor;
    [SerializeField] Color InvalidPlaceColor;

    GraveDatabase.GraveData LastSelectedType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LastSelectedType = GraveDatabase.Instance.GraveList[0];
        PlayerManager.Instance.Input.Player.PlaceGrave.started += AttemptPlaceGrave;
        PlayerManager.Instance.Input.Player.SelectGrave.started += SelectGraveType;
    }

    void SelectGraveType(InputAction.CallbackContext ctx)
    {
        int type = ((int)ctx.ReadValue<float>()) - 1;
        LastSelectedType = GraveDatabase.Instance.GetGraveData(type);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isValidPosition(mousePos))
        {
            GravePreviewLocation.transform.position = GridManager.Instance.ToWorldSpace(GridManager.Instance.ToGridSpace(mousePos));
            GravePreviewLocation.sprite = LastSelectedType.sprite;
            GravePreviewLocation.color = HasEnoughResources() ? ValidPlaceColor : InvalidPlaceColor;
        } else
        {
            GravePreviewLocation.sprite = null;
        }
    }

    bool isValidPosition(Vector2 pos) 
    {
        // is selected grid spot already occupied?
        if (GridManager.Instance.HasGraveAt(pos)) return false;

        Vector3 center = PlayerManager.Instance._collider.bounds.center;
        Vector3 extents = PlayerManager.Instance._collider.bounds.extents;
        Vector2[] corners = new[] {new Vector2(center.x - extents.x, center.y - extents.y),
                                    new Vector2(center.x + extents.x, center.y - extents.y),
                                    new Vector2(center.x - extents.x, center.y + extents.y),
                                    new Vector2(center.x + extents.x, center.y + extents.y) };

        // Is player in grid spot? Don't place on player.
        //Debug.DrawLine(corners[0], corners[3]);
        foreach (var corner in corners)
        {
            if (GridManager.Instance.ToGridSpace(pos) == 
                GridManager.Instance.ToGridSpace(corner)) return false;
        }

        // is it close enough to player?
        foreach (var corner in corners)
        {
            if (Vector2.Distance(GridManager.Instance.ToGridSpace(pos), 
                GridManager.Instance.ToGridSpace(corner)) < 1.5f) return true;
            
        }

        return false;
        
    } 

    void AttemptPlaceGrave(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isValidPosition(mousePos) && HasEnoughResources())
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
        bool hasEnoughSoul = PlayerManager.Instance.playerResources[soulIndex].cost >= LastSelectedType.resourceRequirements[soulIndex].cost;
        bool hasEnoughGrave = PlayerManager.Instance.playerResources[graveIndex].cost >= LastSelectedType.resourceRequirements[graveIndex].cost;
        return hasEnoughSoul && hasEnoughGrave;
    }

    void ChargePlayer()
    {
        PlayerManager.Instance.playerResources[soulIndex].cost -= LastSelectedType.resourceRequirements[soulIndex].cost;
        PlayerManager.Instance.playerResources[graveIndex].cost -= LastSelectedType.resourceRequirements[graveIndex].cost;
    }
}
