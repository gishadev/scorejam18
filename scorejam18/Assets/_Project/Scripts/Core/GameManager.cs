using Gisha.scorejam18.Gameplay;
using Gisha.scorejam18.UI;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance { get; set; }

        private GameTimer _gameTimer;
        private Collector[] _collectors;

        private void Awake()
        {
            Instance = this;
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

        private void OnCollectableAcquired()
        {
            foreach (var collector in _collectors)
            {
                if (!collector.IsReady)
                    return;
            }

            Win();
        }

        public static void Win()
        {
            Debug.Log("You won!");
        }

        public static void Lose()
        {
            Instance._gameTimer.StopTimer();
            UIManager.Instance.ShowLosePopup();

            LeaderboardsSDKController.SubmitScore(PlayerPrefs.GetString("Nickname"), ScoreManager.CurrentScore);
        }
    }
}