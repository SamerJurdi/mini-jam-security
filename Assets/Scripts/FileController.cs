using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Wandering Settings")]
    public float minWanderInterval = 2f; // Minimum time between movements
    public float maxWanderInterval = 5f; // Maximum time between movements
    public float moveSpeed = 2f; // Speed of the NPC
    public float wanderDuration = 1f; // How long the NPC moves in one direction

    [Header("Boundary Settings")]
    public float movementRadius = 5f; // Radius within which the NPC can wander

    private Vector2 startingPosition; // Initial position to limit movement range
    private Rigidbody2D rb;
    private Vector2 wanderDirection;
    private float nextWanderTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ScheduleNextWander();
    }

    void Update()
    {
        // Check if it's time to start wandering
        if (Time.time >= nextWanderTime)
        {
            StartCoroutine(Wander());
            ScheduleNextWander();
        }
    }

    private void ScheduleNextWander()
    {
        nextWanderTime = Time.time + Random.Range(minWanderInterval, maxWanderInterval);
    }

    private System.Collections.IEnumerator Wander()
    {
        // Choose a random direction
        wanderDirection = Random.insideUnitCircle.normalized;

        float elapsedTime = 0f;
        while (elapsedTime < wanderDuration)
        {
            elapsedTime += Time.deltaTime;
            // Calculate potential new position
            Vector2 newPosition = rb.position + wanderDirection * moveSpeed * Time.deltaTime;
            startingPosition = transform.position;

            // Clamp position to stay within the movement radius
            if (Vector2.Distance(newPosition, startingPosition) <= movementRadius)
            {
                rb.MovePosition(newPosition);
            }

            yield return null;
        }

        // Stop movement after wandering
        rb.velocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the movement radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }
}
