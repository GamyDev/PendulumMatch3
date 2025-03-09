using System;
using Match3Game.Balls;
using Match3Game.Utils;
using UnityEngine;

namespace Match3Game.Controllers
{
    public class GameManager : MonoBehaviour
    {
        private ISimpleGrid simpleGrid;
        
        public static Action gameOverEvent;
        
        private void OnEnable()
        {
            SimpleBall.addToGridEvent += OnAddToGridEvent;
        }

        private void Start()
        {
            simpleGrid = GameServices.Get<ISimpleGrid>();
        }

        private void OnAddToGridEvent(SimpleBall obj)
        {
            simpleGrid.CheckMatches();
            
            if (simpleGrid.IsAllRowsFilled())
            {
              gameOverEvent?.Invoke();
            }
        }

        private void OnDisable()
        {
            SimpleBall.addToGridEvent -= OnAddToGridEvent;
        }
    }
}