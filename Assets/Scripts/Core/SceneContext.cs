using UnityEngine;
using Match3Game.BallsFactory;
using Match3Game.Pendulum;
using Match3Game.Services;
using Match3Game.Utils;

namespace  Match3Game.Context
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private AService _ballSpawnerService;
        [SerializeField] private AService _pendulumService;
        [SerializeField] private AService _pendulumLineService;
        [SerializeField] private AService _simpleGrid;
        
        private void Awake()
        {
            RegisterGridService();
            RegisterBallFactory();
            RegisterPendulumService();
            RegisterPendulumLineService();
        }
        private void OnDisable()
        {
            GameServices.Clear();
        }
        private void RegisterGridService()
        {
            var simpleGrid = _simpleGrid.GetController<SimpleGrid>();
            GameServices.Register<ISimpleGrid, SimpleGrid>(simpleGrid); 
        }

        private void RegisterPendulumLineService()
        {
            var pendulumLine = _pendulumLineService.GetController<PendulumLine>();
            GameServices.Register<IPendulumLine, PendulumLine>(pendulumLine);
        }

        private void RegisterPendulumService()
        {
            var pendulum = _pendulumService.GetController<StandartPendulum>();
            GameServices.Register<IPendulum, StandartPendulum>(pendulum);
        }

        private void RegisterBallFactory()
        {
            var ballController = _ballSpawnerService.GetController<BallSpawner>();
            GameServices.Register<IObjectSpawner, BallSpawner>(new BallSpawner(ballController.Prefab));
        }
    }    
}

