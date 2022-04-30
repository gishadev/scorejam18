using UnityEngine;

namespace Gisha.scorejam18
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private int collectablesCountTarget = 3;

        private int _collectablesCount;


        private void CheckCollectables()
        {
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
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                _collectablesCount--;
                CheckCollectables();
            }
        }
    }
}