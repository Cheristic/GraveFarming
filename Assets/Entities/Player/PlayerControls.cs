using UnityEngine;
using UnityEngine.InputSystem;
using static GlobalLayers;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float MoveSpeed;

    internal PlayerInput input;
    Rigidbody2D rb;

    public void Init()
    {
        input = new();
        rb = GetComponent<Rigidbody2D>();
        input.Enable();
        OnEnable();
    }

    private void OnEnable()
    {
        if (input != null) input.Player.BreakGrave.started += AttemptBreakGraveFromMouse;
    }

    private void OnDisable()
    {
        input?.Disable();
        input.Player.BreakGrave.started -= AttemptBreakGraveFromMouse;
    }

    void FixedUpdate()
    {
        Vector2 inputDir = input.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = inputDir * MoveSpeed;
    }

    void AttemptBreakGraveFromMouse(InputAction.CallbackContext ctx)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, .001f, GlobalLayers.GetLayerMask(Layers.Grave));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent<Grave>(out var grave))
            {
                grave.Break();
            }
        }
    }
}
