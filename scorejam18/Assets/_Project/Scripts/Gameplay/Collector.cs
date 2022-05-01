using System;
using Gisha.scorejam18.Core;
using TMPro;
using UnityEngine;

namespace Gisha.scorejam18.Gameplay
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private int collectablesCountTarget = 3;
        [SerializeField] private TMP_Text countText;

        public bool IsReady { get; private set; }
        public static Action CollectableAcquired;

        private int _collectablesCount;

        private void CheckCollectables()
        {
            countText.text = $"{_collectablesCount} of {collectablesCountTarget}";
            if (_collectablesCount >= collectablesCountTarget)
                IsReady = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsReady)
                return;

            if (other.CompareTag("Collectable"))
            {
                _collectablesCount++;
                
                var collectable = other.GetComponent<Collectable>();
                ScoreManager.AddScore(collectable.Price);
                CheckCollectables();
                
                CollectableAcquired?.Invoke();

                Destroy(other.gameObject);
            }
        }
    }
}