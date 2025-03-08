using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    [SerializeField] private int columnIndex; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Ball>())
        {
            Ball ball = other.GetComponent<Ball>();
            if (ball != null)
            {
                GameGrid.Instance.AddBallToGrid(ball, columnIndex);
            }
        }
    }
}
