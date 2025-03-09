using Match3Game.Balls;
using Match3Game.Utils;
using UnityEngine;
using TMPro;

namespace Match3Game.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public int Score { get; private set; }
        private int highScore;
        [SerializeField] private TextMeshProUGUI textScore;
        [SerializeField] private TextMeshProUGUI textHighScore;
        private void OnEnable()
        {
            SimpleGrid.destroyBallEvent += OnDestroyBallEvent;
        }

        private void Start()
        {
            highScore = PlayerPrefs.GetInt("highScore", highScore);
            ShowScore();
        }

        private void OnDestroyBallEvent(SimpleBall ball)
        {
            switch (ball.Colorable.ColorID)
            {
                case 0:
                    AddScore(1);
                    break;
                case 1:
                    AddScore(2);
                    break;
                case 2:
                    AddScore(3);
                    break;
            }

        }

        public void ShowScore()
        {
            textScore.text = Score.ToString();
            textHighScore.text = highScore.ToString();
        }

        private void AddScore(int value)
        {
           Score += value;

            if (Score > highScore)
            {
                highScore = Score;

                PlayerPrefs.SetInt("highScore", highScore);
            }



            ShowScore();
        }

        private void OnDisable()
        {
            SimpleGrid.destroyBallEvent -= OnDestroyBallEvent;
        }
    }
}