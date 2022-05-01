using TMPro;
using UnityEngine;

namespace Gisha.scorejam18
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private int collectablesCountTarget = 3;
        [SerializeField] private TMP_Text countText;

        private int _collectablesCount;


        private void CheckCollectables()
        {
            countText.text = $"{_collectablesCount} of {collectablesCountTarget}";
            if (_collectablesCount >= collectablesCountTarget)
            {
                Debug.Log("Collector is ready!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                _collectablesCount++;
                CheckCollectables();

                Destroy(other.gameObject);
            }
        }
    }
}