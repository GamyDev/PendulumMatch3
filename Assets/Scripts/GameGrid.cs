using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameGrid : MonoBehaviour
{
    public static GameGrid Instance;
    private int columns = 3;
    private int rows = 3;
    [SerializeField] private Transform[] dropZones;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float ballSpacing;
    [SerializeField] private float yOffset; 

    private Ball[,] grid;

    private void Awake()
    {
        Instance = this;
        grid = new Ball[columns, rows];
    }

    public void AddBallToGrid(Ball ball, int column)
    {
       
        int row = GetEmptyRow(column);
        
        Debug.Log($"Adding ball to grid: {column}, {row}");
        if (row == -1) return; 

        float yPos = dropZones[column].position.y + row * ballSpacing + yOffset;
        grid[column, row] = ball;
        ball.transform.position = new Vector2(dropZones[column].position.x, yPos);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        CheckMatches();
        CheckGameOver();
    }

    private int GetEmptyRow(int column)
    {
        for (int i = 0; i < rows; i++)
        {
            if (grid[column, i] == null)
                return i;
        }
        return -1;
    }

    private void CheckMatches()
    {
        List<Ball> toDestroy = new List<Ball>();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (grid[x, y] == null) continue;
                int color = grid[x, y].colorID;

                if (x <= columns - 3 && grid[x + 1, y]?.colorID == color && grid[x + 2, y]?.colorID == color)
                {
                    toDestroy.Add(grid[x, y]);
                    toDestroy.Add(grid[x + 1, y]);
                    toDestroy.Add(grid[x + 2, y]);
                }

                if (y <= rows - 3 && grid[x, y + 1]?.colorID == color && grid[x, y + 2]?.colorID == color)
                {
                    toDestroy.Add(grid[x, y]);
                    toDestroy.Add(grid[x, y + 1]);
                    toDestroy.Add(grid[x, y + 2]);
                }

                if (x <= columns - 3 && y <= rows - 3 && grid[x + 1, y + 1]?.colorID == color && grid[x + 2, y + 2]?.colorID == color)
                {
                    toDestroy.Add(grid[x, y]);
                    toDestroy.Add(grid[x + 1, y + 1]);
                    toDestroy.Add(grid[x + 2, y + 2]);
                }

                if (x >= 2 && y <= rows - 3 && grid[x - 1, y + 1]?.colorID == color && grid[x - 2, y + 2]?.colorID == color)
                {
                    toDestroy.Add(grid[x, y]);
                    toDestroy.Add(grid[x - 1, y + 1]);
                    toDestroy.Add(grid[x - 2, y + 2]);
                }
            }
        }

        if (toDestroy.Count > 0)
        {
            foreach (Ball ball in toDestroy)
            {
                Instantiate(explosionEffect, ball.transform.position, Quaternion.identity);
                Destroy(ball.gameObject);
                RemoveBallFromGrid(ball);
            }

            StartCoroutine(ApplyGravity());
        }
    }

    private void RemoveBallFromGrid(Ball ball)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (grid[x, y] == ball)
                {
                    grid[x, y] = null;
                    return;
                }
            }
        }
    }

    private IEnumerator ApplyGravity()
    {
        yield return new WaitForSeconds(0.2f);

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                if (grid[x, y] == null)
                {
                    for (int above = y + 1; above < rows; above++)
                    {
                        if (grid[x, above] != null)
                        {
                            grid[x, y] = grid[x, above];
                            grid[x, above] = null;

                            float yPos = dropZones[x].position.y + y * ballSpacing + yOffset;
                            grid[x, y].transform.position = new Vector2(dropZones[x].position.x, yPos);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void CheckGameOver()
    {
        for (int x = 0; x < columns; x++)
        {
            if (grid[x, rows - 1] == null) return; 
        }

        GameOver.Instance.ShowGameOver();
    }
}
