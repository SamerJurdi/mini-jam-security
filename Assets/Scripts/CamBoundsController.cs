using UnityEngine;

public class CamBoundsController : MonoBehaviour
{
    private BoxCollider2D topWall, bottomWall, leftWall, rightWall;

    void Start()
    {
        CreateBounds();
    }

    void CreateBounds()
    {
        // Get the camera's dimensions in world space
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;

        Vector3 cameraPosition = cam.transform.position;

        // Create boundaries based on camera's view
        CreateBoundaryWalls(cameraPosition, cameraWidth, cameraHeight);
    }

    void CreateBoundaryWalls(Vector3 cameraPosition, float cameraWidth, float cameraHeight)
    {
        // Create the top boundary
        topWall = CreateBoundary(cameraPosition + new Vector3(0, cameraHeight / 2, 0), cameraWidth, 0.1f);

        // Create the bottom boundary
        bottomWall = CreateBoundary(cameraPosition + new Vector3(0, -cameraHeight / 2, 0), cameraWidth, 0.1f);

        // Create the left boundary
        leftWall = CreateBoundary(cameraPosition + new Vector3(-cameraWidth / 2, 0, 0), 0.1f, cameraHeight);

        // Create the right boundary
        rightWall = CreateBoundary(cameraPosition + new Vector3(cameraWidth / 2, 0, 0), 0.1f, cameraHeight);
    }

    BoxCollider2D CreateBoundary(Vector3 position, float width, float height)
    {
        GameObject boundary = new GameObject("BoundaryWall");
        boundary.transform.position = position;

        BoxCollider2D collider = boundary.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(width, height);

        Rigidbody2D rb = boundary.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;  // Don't allow physics forces to move the boundary

        return collider;
    }

    // Optionally, update the boundaries when the camera moves
    void Update()
    {
        UpdateBounds();
    }

    void UpdateBounds()
    {
        Camera cam = Camera.main;
        float cameraHeight = cam.orthographicSize * 2;
        float cameraWidth = cameraHeight * cam.aspect;
        Vector3 cameraPosition = cam.transform.position;

        topWall.transform.position = cameraPosition + new Vector3(0, cameraHeight / 2, 0);
        bottomWall.transform.position = cameraPosition + new Vector3(0, -cameraHeight / 2, 0);
        leftWall.transform.position = cameraPosition + new Vector3(-cameraWidth / 2, 0, 0);
        rightWall.transform.position = cameraPosition + new Vector3(cameraWidth / 2, 0, 0);
    }
}
