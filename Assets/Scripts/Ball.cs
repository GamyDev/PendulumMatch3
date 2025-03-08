using UnityEngine;

public class Ball : MonoBehaviour
{
    public int colorID; 
    private bool isFalling = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Color[] colors = new Color[3] { Color.red, Color.blue, Color.green }; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.isKinematic = true; 

        SetColor(); 
    }

    public void AttachToPendulum(Transform pendulum)
    {
        transform.SetParent(pendulum);
        transform.localPosition = Vector3.zero; 
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    public void DropBall()
    {
        if (!isFalling)
        {
            isFalling = true;
            transform.SetParent(null); 
            rb.isKinematic = false; 
            rb.velocity = Vector2.zero;
        }
    }

    public void SetColor()
    {
        if (sr != null)
        {
            sr.color = colors[colorID]; 
        }
    }
}
