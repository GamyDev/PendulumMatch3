using System;
using Match3Game.Balls;
using Match3Game.Utils;
using UnityEngine;

namespace Match3Game.Controllers
{
    public class ExplosionEffects : MonoBehaviour
    {
        private void OnEnable()
        {
            SimpleGrid.destroyBallEvent += OnDestroyBallEvent;
        }

        private void OnDestroyBallEvent(SimpleBall obj)
        {
            
        }

        private void OnDisable()
        {
            SimpleGrid.destroyBallEvent -= OnDestroyBallEvent;
        }
    }
}