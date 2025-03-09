using System;
using Match3Game.Pendulum;
using UnityEngine;

namespace Match3Game.Services
{
    public class PendelumLineService : AService
    {
        [SerializeField] private LineRenderer _lineRenderer;
        
        public override T GetController<T>()
        {
            if (typeof(T) == typeof(PendulumLine))
            {
                var pendulum = GameServices.Get<IPendulum>();
                if (pendulum == null) throw new NullReferenceException();
                
                return new PendulumLine(_lineRenderer, pendulum) as T;
            }

            return null;
        }
    }
}