using System;
using Match3Game.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using NTC.Pool;
using System.Collections;

namespace Match3Game.Balls
{
    public interface IColorable
    {
        int ColorID { get; }
        Color[] Colors { get; }
        
        void SetColor(int colorID);
    }

    public interface IDropable
    {
        void Drop();
    }

    public interface IGridObject
    {
        void AddToGrid(ISimpleGrid grid, int column, int row);
        void RemoveFromGrid(SimpleGrid grid);
    }

    public class SimpleBall : MonoBehaviour, IGridObject
    {   
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private SpriteRenderer spriteRendererr;
        [SerializeField] private GameObject particleObject;
        public bool Disabled { get; set; }
        
        private IColorable colorable;
        private IDropable dropable;
        
        public static event Action<SimpleBall> addToGridEvent;

        public IColorable Colorable => colorable;

        public void Start()
        {
            colorable =  new BallColorer(spriteRendererr);
            dropable = new BallDropper(rb2d);

            var colorsLength = Colorable.Colors.Length;
            Colorable.SetColor(Random.Range(0, colorsLength));
        }

        public void AddToGrid(ISimpleGrid grid, int column, int row)
        { 
            if (row == -1) return; 
            
            var rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            
            Debug.Log($"Adding ball to grid: {column}, {row}");

            var yPos = grid.DropZones[column].position.y + row * grid.BallSpacing + grid.YOffset;
            
            grid.SimpleBallArray[column, row] = this;
            transform.position = new Vector2(grid.DropZones[column].position.x, yPos); 
            
            addToGridEvent?.Invoke(this);
        }

        public void RemoveFromGrid(SimpleGrid grid)
        {
            for (int x = 0; x < grid.Columns; x++)
            {
                for (int y = 0; y < grid.Rows; y++)
                {
                    if (grid.SimpleBallArray[x, y] == this)
                    {
                        grid.SimpleBallArray[x, y] = null;
                        return;
                    }
                }
            }
        }

        public void DestroyObject()
        {
            //  GameObject item = NightPool.Spawn(particleObject, transformPoint);
            //   item.transform.position = transform.position;


            StartCoroutine(SpawnObjectWithDelay());
            //  Destroy(gameObject);
        }

        IEnumerator SpawnObjectWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            NightPool.Spawn(particleObject);
            NightPool.Despawn(gameObject);
        }
    }
}