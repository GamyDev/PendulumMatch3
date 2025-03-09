using Match3Game.BallsFactory;
using Match3Game.Pendulum;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Match3Game.Controllers
{
    public class PendeliumController : MonoBehaviour
    {
        private IPendulum _pendulum;
        private IPendulumLine _pendulumLine;
        private IObjectSpawner _ballSpawner;
        
        private bool hasBall = false;

        private GameObject _currentBall;
        
        private void Start()
        {
            _ballSpawner = GameServices.Get<IObjectSpawner>();
            _pendulum = GameServices.Get<IPendulum>();
            _pendulumLine = GameServices.Get<IPendulumLine>();
            
            SpawnBall();
        }

        private void SpawnBall()
        {
            if(hasBall) return;
            
            _currentBall = _ballSpawner.CreateObject(_pendulum.EndPoint.position, _pendulum.EndPoint);

            hasBall = true;
        }
        
        private void DropBall()
        {
            if (!hasBall) return;
            
            _currentBall.transform.SetParent(null);
            DisablePhysics();
            hasBall = false;
                
            Invoke(nameof(SpawnBall), 1.5f);
        }

        private void DisablePhysics()
        {
            var rigidbody2d = _currentBall.GetComponent<Rigidbody2D>();
            rigidbody2d.isKinematic = false;
            rigidbody2d.gravityScale = 1;
        }

        private void Update()
        {
            _pendulum.Update(Time.deltaTime);
            _pendulumLine.UpdateLine();

            if (!IsPointerOverUI())
            {
                HandleInputs();
            }
        }
        private bool IsPointerOverUI()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
        private void HandleInputs()
        {


            if (Input.GetMouseButton(0))
            {
                DropBall();
            }
        }
    }    
}

