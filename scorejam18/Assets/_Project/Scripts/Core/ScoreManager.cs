using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager Instance { get; set; }

        [SerializeField] private TMP_Text moneyText;

        public static int CurrentScore { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static void AddScore(int count)
        {
            CurrentScore += count;
            Instance.moneyText.text = "$" + CurrentScore;
        }
    }
}