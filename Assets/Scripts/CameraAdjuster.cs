using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public Camera cam;

    private float baseHeight = 1920f;
    private float minY = 1f, maxY = 3f;
    private float minSize = 5.8f, maxSize = 7.85f;
    private float maxHeight = 2520f;

    void Start()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        float screenHeight = Screen.height;

        if (screenHeight <= baseHeight)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, minY, cam.transform.position.z);
            cam.orthographicSize = minSize;
        }
        else if (screenHeight >= maxHeight)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, maxY, cam.transform.position.z);
            cam.orthographicSize = maxSize;
        }
        else
        {
            float t = (screenHeight - baseHeight) / (maxHeight - baseHeight);
            float newY = Mathf.Lerp(minY, maxY, t);
            float newSize = Mathf.Lerp(minSize, maxSize, t);

            cam.transform.position = new Vector3(cam.transform.position.x, newY, cam.transform.position.z);
            cam.orthographicSize = newSize;
        }
    }
}
