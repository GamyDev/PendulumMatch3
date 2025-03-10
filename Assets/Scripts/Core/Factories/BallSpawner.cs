using NTC.Pool;
using UnityEngine;


namespace Match3Game.BallsFactory
{
    public interface IObjectSpawner 
    {
        GameObject CreateObject(Vector3 position, Transform parent);
    }
    
    public class BallSpawner : IObjectSpawner
    {
        private GameObject _prefab;
        public GameObject Prefab => _prefab;
    
        public BallSpawner(GameObject prefab)
        {
            _prefab = prefab;
        }
        
        public GameObject CreateObject(Vector3 position, Transform parent)
        {
            return NightPool.Spawn(Prefab, position, Quaternion.identity, parent);
        }
    }    
}

