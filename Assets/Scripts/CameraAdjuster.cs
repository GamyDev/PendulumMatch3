using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private float baseAspect = 1920f / 1080f; 
    private float maxAspect = 2520f / 1080f; 

    private float minY = 1f, maxY = 3f;
    private float minSize = 5.8f, maxSize = 7.85f;

    private int lastScreenWidth, lastScreenHeight;

    void Start()
    {
        AdjustCamera();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            AdjustCamera();
        }
    }

    void AdjustCamera()
    {
        Debug.Log("AdjustCamera");
        if (cam == null)
        {
            cam = Camera.main;
        }

        float currentAspect = (float)Screen.height / Screen.width; 

        if (currentAspect <= baseAspect)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, minY, cam.transform.position.z);
            cam.orthographicSize = minSize;
        }
        else if (currentAspect >= maxAspect)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, maxY, cam.transform.position.z);
            cam.orthographicSize = maxSize;
        }
        else
        {
            float t = (currentAspect - baseAspect) / (maxAspect - baseAspect);
            float newY = Mathf.Lerp(minY, maxY, t);
            float newSize = Mathf.Lerp(minSize, maxSize, t);

            cam.transform.position = new Vector3(cam.transform.position.x, newY, cam.transform.position.z);
            cam.orthographicSize = newSize;
        }
    }
}
