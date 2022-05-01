using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class GameTimer : MonoBehaviour
    {
        [Header("General")] [SerializeField] private float maxRoundTimeInSeconds;

        [Header("UI")] [SerializeField] private TMP_Text timeText;
        [SerializeField] private Transform timeBar;

        public Action TimeOut;

        private float _currentTime;

        public void StartTimer()
        {
            _currentTime = maxRoundTimeInSeconds;
            StartCoroutine(TimerCoroutine());
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                _currentTime -= Time.deltaTime;

                if (_currentTime <= 0)
                    TimeOut?.Invoke();

                UpdateUI();

                yield return null;
            }
        }

        private void UpdateUI()
        {
            timeText.text = Mathf.Round(_currentTime).ToString();
            timeBar.transform.localScale = new Vector3(_currentTime / maxRoundTimeInSeconds, 1f, 1f);
        }
    }
}