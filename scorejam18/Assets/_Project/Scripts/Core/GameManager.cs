using System.Collections;
using Gisha.scorejam18.Gameplay;
using Gisha.scorejam18.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.scorejam18.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; set; }

        private LeaderboardController _leaderboardController;
        private GameTimer _gameTimer;
        private Collector[] _collectors;

        private void Awake()
        {
            Instance = this;
            _leaderboardController = FindObjectOfType<LeaderboardController>();
            _collectors = FindObjectsOfType<Collector>();
            _gameTimer = GetComponent<GameTimer>();
        }

        private void Start()
        {
            _gameTimer.StartTimer();
        }

        private void OnEnable()
        {
            Collector.CollectableAcquired += OnCollectableAcquired;
            _gameTimer.TimeOut += Lose;
        }

        private void OnDisable()
        {
            Collector.CollectableAcquired -= OnCollectableAcquired;
            _gameTimer.TimeOut -= Lose;
        }

        public static void Win()
        {
            Instance._gameTimer.StopTimer();
            UIManager.Instance.ShowWinPopup();
        }

        public static void Lose()
        {
            Instance.StartCoroutine(Instance.LoseRoutine());
        }

        private void OnCollectableAcquired()
        {
            foreach (var collector in _collectors)
            {
                if (!collector.IsReady)
                    return;
            }

            Win();
        }

        public void LoadRandomLevel()
        {
            int level = Random.Range(1, 4);
            SceneManager.LoadScene("Level" + level);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }


        private IEnumerator LoseRoutine()
        {
            Instance._gameTimer.StopTimer();
            UIManager.Instance.ShowLosePopup();

            yield return _leaderboardController.SubmitScoreRoutine(PlayerManager.CurrentScore);
            PlayerManager.CurrentScore = 0;
        }
    }
}