using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;

    private bool hasBall = false;
    private GameObject currentBall;

    [SerializeField] private Transform pivot; 
    [SerializeField] private float radius; 
    [SerializeField] private float minAngle; 
    [SerializeField] private float maxAngle; 
    [SerializeField] private float speed;
    [SerializeField] private float objectHeight; 

    private float time;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; 
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true;
        SpawnBall();
    }



    void Update()
    {
        if (pivot == null) return;

        time += Time.deltaTime * speed;
        float angle = Mathf.Lerp(minAngle, maxAngle, (Mathf.Sin(time) + 1) / 2);
        float angleRad = angle * Mathf.Deg2Rad;

        Vector2 offset = new Vector2(Mathf.Sin(angleRad), -Mathf.Cos(angleRad)) * radius;
        transform.position = (Vector2)pivot.position + offset;

        Vector2 attachmentPoint = transform.position + new Vector3(0, objectHeight, 0);

        lineRenderer.SetPosition(0, pivot.position); 
        lineRenderer.SetPosition(1, attachmentPoint); 


        if (Input.GetMouseButtonDown(0))
        {
            DropBall();
        }
    }



    void SpawnBall()
    {
        if (hasBall) return;

        currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        currentBall.transform.SetParent(transform);
        currentBall.transform.localPosition = Vector3.zero;

        Ball ballScript = currentBall.GetComponent<Ball>();
        ballScript.colorID = Random.Range(0, 3); 

        hasBall = true;
    }


    public void DropBall()
    {
        if (hasBall)
        {
            currentBall.transform.SetParent(null); 
            Rigidbody2D rb = currentBall.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.gravityScale = 1; 
            hasBall = false;
            Invoke("SpawnBall", 1.5f); 
        }
    }
}
