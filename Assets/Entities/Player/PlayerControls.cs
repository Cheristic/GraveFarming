using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float MoveSpeed;

    PlayerInput input;
    Rigidbody2D rb;

    public void Init()
    {
        input = new();
        rb = GetComponent<Rigidbody2D>();
        input.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }

    void FixedUpdate()
    {
        Vector2 inputDir = input.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = inputDir * MoveSpeed;
    }

}
