using UnityEngine;

namespace Match3Game.Services
{
    public abstract class AService : MonoBehaviour
    {
        public abstract T GetController<T>() where T : class;
    }
}