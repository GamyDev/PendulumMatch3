using System;
using System.Collections;
using System.Collections.Generic;
using Match3Game.Balls;
using UnityEngine;

namespace Match3Game.Utils
{
    public interface ISimpleGrid
    {
        SimpleBall[,] SimpleBallArray { get; }
        Transform[] DropZones { get; }
        float BallSpacing { get; }
        float YOffset { get; }
        
        bool IsAllRowsFilled();
        void CheckMatches();
        int GetEmptyRow(int column);
    }
    
    public class SimpleGrid : MonoBehaviour, ISimpleGrid
    {
        private int columns = 3;
        private int rows = 3;
        
        [SerializeField] private Transform[] dropZones;
        [SerializeField] private float ballSpacing;
        [SerializeField] private float yOffset; 
        
        private SimpleBall[,] grid;

        public static Action<SimpleBall> destroyBallEvent;
        
        public int Columns => columns;
        public int Rows => rows;

        public SimpleBall[,] SimpleBallArray => grid;

        public Transform[] DropZones => dropZones;

        public float BallSpacing => ballSpacing;

        public float YOffset => yOffset;

        private void Start()
        {
            grid = new SimpleBall[columns, rows];
        }
        
        public int GetEmptyRow(int column)
        {
            for (int i = 0; i < rows; i++)
            {
                if (grid[column, i] == null)
                    return i;
            }
            return -1;
        }
        
        
        public void CheckMatches()
            {
                List<SimpleBall> toDestroy = new();
        
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        if (grid[x, y] == null) continue;
                        int color = grid[x, y].Colorable.ColorID;
        
                        if (x <= columns - 3 && grid[x + 1, y]?.Colorable.ColorID == color && grid[x + 2, y]?.Colorable.ColorID == color)
                        {
                            toDestroy.Add(grid[x, y]);
                            toDestroy.Add(grid[x + 1, y]);
                            toDestroy.Add(grid[x + 2, y]);
                        }
        
                        if (y <= rows - 3 && grid[x, y + 1]?.Colorable.ColorID == color && grid[x, y + 2]?.Colorable.ColorID == color)
                        {
                            toDestroy.Add(grid[x, y]);
                            toDestroy.Add(grid[x, y + 1]);
                            toDestroy.Add(grid[x, y + 2]);
                        }
        
                        if (x <= columns - 3 && y <= rows - 3 && grid[x + 1, y + 1]?.Colorable.ColorID == color && grid[x + 2, y + 2]?.Colorable.ColorID == color)
                        {
                            toDestroy.Add(grid[x, y]);
                            toDestroy.Add(grid[x + 1, y + 1]);
                            toDestroy.Add(grid[x + 2, y + 2]);
                        }
        
                        if (x >= 2 && y <= rows - 3 && grid[x - 1, y + 1]?.Colorable.ColorID == color && grid[x - 2, y + 2]?.Colorable.ColorID == color)
                        {
                            toDestroy.Add(grid[x, y]);
                            toDestroy.Add(grid[x - 1, y + 1]);
                            toDestroy.Add(grid[x - 2, y + 2]);
                        }
                    }
                }
        
                if (toDestroy.Count > 0)
                {
                    foreach (SimpleBall ball in toDestroy)
                    {
                        destroyBallEvent?.Invoke(ball);
                        
                        ball.DestroyObject();
                        ball.RemoveFromGrid(this);
                    }
        
                    StartCoroutine(ApplyGravity());
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


        public bool IsAllRowsFilled()
        {
            for (int x = 0; x < columns; x++)
            {
                if (grid[x, rows - 1] == null) return false; 
            }

            return true;
        }
    }
}