using System;
using System.Collections;
using Gisha.scorejam18.Core;
using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.MainMenu
{
    public class TownLeaderboards : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] playerNames = new TMP_Text[15];
        [SerializeField] private TMP_Text[] playerScores = new TMP_Text[15];

        private LeaderboardController _leaderboardController;

        private void Awake()
        {
            _leaderboardController = FindObjectOfType<LeaderboardController>();
        }

        private void Start()
        {
            StartCoroutine(SetupRoutine());
        }

        private IEnumerator SetupRoutine()
        {
            yield return _leaderboardController.FetchTopHighScoresRoutine();

            for (int i = 0; i < 15; i++)
            {
                if (_leaderboardController.PlayerScores[i] == null || _leaderboardController.PlayerNames[i] == null)
                    continue;

                playerNames[i].text = _leaderboardController.PlayerNames[i];
                playerScores[i].text = "$" + _leaderboardController.PlayerScores[i];
            }
        }
    }
}