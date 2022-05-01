using UnityEngine;

namespace Gisha.scorejam18.Utilities
{
    public class FPSLimiter : MonoBehaviour
    {
        [SerializeField] private int maxFps = 60;

        private void Awake()
        {
            Application.targetFrameRate = maxFps;
        }
    }
}