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
        private TMP_Text loseCurrentScoreText;

        [SerializeField] private TMP_Text highScoreText;

        [Header("Win Popup")] [SerializeField] private TMP_Text winCurrentScoreText;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowLosePopup()
        {
            loseCurrentScoreText.text = "$" + PlayerManager.CurrentScore;
            // highScoreText.text = "$" + LeaderboardController.GetHighScore(PlayerPrefs.GetString("Nickname"));
            losePopup.SetActive(true);
        }

        public void ShowWinPopup()
        {
            winCurrentScoreText.text = "$" + PlayerManager.CurrentScore;
            winPopup.SetActive(true);
        }

        public void OnClick_ReturnToMenu()
        {
            GameManager.Instance.LoadMenu();
        }

        public void OnClick_NextRound()
        {
            GameManager.Instance.LoadRandomLevel();
        }
    }
}