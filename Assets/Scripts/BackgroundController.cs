using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public Camera mainCamera; // Drag your main camera here in the Inspector

    void Start()
    {
        ScaleBackground();
    }

    void ScaleBackground()
    {
        // Get the sprite's aspect ratio
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        // Get the camera's aspect ratio and size
        float cameraAspect = mainCamera.aspect;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * cameraAspect;

        // Calculate scale factors to preserve the aspect ratio
        float scaleX = cameraWidth / spriteWidth;
        float scaleY = cameraHeight / spriteHeight;

        // Choose the scale factor that will fill the screen while preserving the aspect ratio
        // We use Mathf.Max to ensure that the image covers the whole screen, even if it overflows
        float scale = Mathf.Max(scaleX, scaleY);

        // Apply the scaling while preserving the aspect ratio
        transform.localScale = new Vector3(scale, scale, 1);

        // Position the background to ensure it stays centered
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}
