using Gisha.scorejam18.Gameplay;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance { get; set; }

        private Collector[] _collectors;
        
        private void Awake()
        {
            Instance = this;
            _collectors = FindObjectsOfType<Collector>();
        }

        private void OnEnable()
        {
            Collector.CollectableAcquired += OnCollectableAcquired;
        }

        private void OnDisable()
        {
            Collector.CollectableAcquired -= OnCollectableAcquired;
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
            Debug.Log("You lose!");
        }
    }
}