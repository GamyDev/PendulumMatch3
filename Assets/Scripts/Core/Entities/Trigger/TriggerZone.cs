using Match3Game.Balls;
using Match3Game.Utils;
using UnityEngine;

namespace Match3Game.Trigger
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private int columnIndex;

        private ISimpleGrid simpleGrid;
        
        
        private void Start()
        {
            simpleGrid = GameServices.Get<ISimpleGrid>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<SimpleBall>())
            { 
                var ball = other.GetComponent<SimpleBall>();
                if (ball != null)
                {
                    if(ball.Disabled) return;
                    
                    ball.Disabled = true;
                    
                    var row = simpleGrid.GetEmptyRow(columnIndex);
                    ball.AddToGrid(simpleGrid, columnIndex, row);
                } 
            }
        }
    }
}