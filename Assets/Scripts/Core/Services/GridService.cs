using Match3Game.Utils;
using UnityEngine;

namespace Match3Game.Services
{
    public class GridService : AService
    {
        [SerializeField] private SimpleGrid simpleGrid;
        
        public override T GetController<T>()
        {
            if (typeof(T) == typeof(SimpleGrid))
                return simpleGrid as T;

            return null;
        }
    }
}