using System;
using Gisha.scorejam18.Core;
using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject losePopup;
        [SerializeField] private GameObject winPopup;

        [Header("Lose Popup")] [SerializeField]
        private TMP_Text currentScoreText;

        [SerializeField] private TMP_Text highScoreText;


        private void Awake()
        {
            Instance = this;
        }

        public void ShowLosePopup()
        {
            currentScoreText.text = "$" + ScoreManager.CurrentScore;
            highScoreText.text = "$" + ScoreManager.CurrentScore;
            losePopup.SetActive(true);
        }

        public void ShowWinPopup()
        {
        }

        public void OnClick_ReturnToMenu()
        {
        }

        public void OnClick_NextRound()
        {
        }
    }
}