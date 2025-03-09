using Match3Game.BallsFactory;
using UnityEngine;

namespace Match3Game.Services
{
    public class BallSpawnerService : AService
    {
        [SerializeField] private GameObject prefab;


        public override T GetController<T>()
        {
            if (typeof(T) == typeof(BallSpawner))
                return new BallSpawner(prefab) as T;

            return null;
        }
    }
}