using System;
using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager Instance { get; set; }

        [SerializeField] private TMP_Text moneyText;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            moneyText.text = "$" + PlayerManager.CurrentScore;
        }

        public static void AddScore(int count)
        {
            PlayerManager.CurrentScore += count;
            Instance.moneyText.text = "$" + PlayerManager.CurrentScore;
        }
    }
}