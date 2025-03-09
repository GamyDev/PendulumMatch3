using Match3Game.Pendulum;
using UnityEngine;

namespace Match3Game.Services 
{
    public class PendulumService : AService
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float radius;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float speed;
        [SerializeField] private float objectHeight;

        public override T GetController<T>()
        {
            if (typeof(T) == typeof(StandartPendulum))
                return new StandartPendulum(pivot, endPoint, radius, minAngle, maxAngle, speed, objectHeight) as T;

            return null;
        }
    }    
}

