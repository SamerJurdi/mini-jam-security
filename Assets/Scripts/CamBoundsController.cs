using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera mainCamera; // Reference to your camera
    private BoxCollider2D cameraBoundsCollider; // BoxCollider2D to restrict movement

    void Start()
    {
        // Create and attach the BoxCollider2D at runtime if not already set up
        if (mainCamera != null)
        {
            CreateBoundsCollider();
        }
    }

    void CreateBoundsCollider()
    {
        // Create a BoxCollider2D if it doesn't exist
        cameraBoundsCollider = gameObject.GetComponent<BoxCollider2D>();
        if (cameraBoundsCollider == null)
        {
            cameraBoundsCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        // Set the collider as a trigger
        cameraBoundsCollider.isTrigger = true;

        // Calculate the camera's world space boundaries
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Position the collider at the camera's center
        transform.position = mainCamera.transform.position;

        // Set the size of the collider to match the camera's visible area
        cameraBoundsCollider.size = new Vector2(cameraWidth, cameraHeight);
    }

    // Optional: You can manually set the camera if you want to change it in runtime
    public void SetCamera(Camera newCamera)
    {
        mainCamera = newCamera;
        CreateBoundsCollider();
    }
}
