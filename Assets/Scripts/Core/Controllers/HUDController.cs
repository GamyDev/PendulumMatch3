using Match3Game.Utils;
using UnityEngine;

namespace Match3Game.Controllers
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        private ISimpleGrid simpleGrid;
        
        private void Start()
        {
            simpleGrid = GameServices.Get<ISimpleGrid>();
        }

        private void OnEnable()
        {
            GameManager.gameOverEvent += ShowGameOver;
        }

        private void OnDisable()
        {
            GameManager.gameOverEvent -= ShowGameOver;
        }

        void ShowGameOver()
        {

            gameOverPanel.SetActive(true);
        }
    }
}