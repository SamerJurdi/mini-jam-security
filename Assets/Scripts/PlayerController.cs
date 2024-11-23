using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float decelerationTime = 0.1f; // Controls the deceleration speed (higher is slower stop)
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private Vector2 targetMovement; // The target movement direction

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the arrow keys or WASD keys for horizontal and vertical movement
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        float moveY = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        // Store the target movement vector and normalize it
        targetMovement = new Vector2(moveX, moveY).normalized; // Normalize to ensure consistent speed even when moving diagonally
    }

    void FixedUpdate()
    {
        // Apply movement instantly for acceleration
        if (targetMovement != Vector2.zero)
        {
            // Move the player directly to the target movement direction (instant acceleration)
            rb.velocity = targetMovement * moveSpeed;
        }
        else
        {
            // Apply deceleration when stopping (smooth deceleration)
            rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref targetMovement, decelerationTime);
        }
    }
}
