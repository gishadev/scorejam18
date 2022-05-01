using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gisha.scorejam18.Core
{
    public class GameTimer : MonoBehaviour
    {
        [Header("General")] [SerializeField] private float maxRoundTimeInSeconds;

        [Header("UI")] [SerializeField] private Sprite emptySquare;
        [SerializeField] private Sprite filledSquare;
        [SerializeField] private Image[] timeImages;

        public Action TimeOut;

        private float _currentTime;

        public void StartTimer()
        {
            _currentTime = maxRoundTimeInSeconds;
            StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            StopAllCoroutines();
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
            int activeCount = Mathf.FloorToInt(_currentTime / maxRoundTimeInSeconds * timeImages.Length);

            for (int i = 0; i < timeImages.Length; i++)
            {
                timeImages[i].sprite = i <= activeCount ? filledSquare : emptySquare;
            }
        }
    }
}