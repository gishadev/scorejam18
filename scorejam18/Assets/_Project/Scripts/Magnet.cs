using System;
using UnityEngine;

namespace Gisha.scorejam18
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private Transform magnetOrigin;
        [SerializeField] private float magnetEffectDistance;
        [SerializeField] private float magnetEffectRadius;
        [SerializeField] private float noBreakDistance;

        private bool _isActive;

        public bool IsActive => _isActive;

        private void Update()
        {
            if (!IsActive)
                return;

            var raycastHits = Physics.SphereCastAll(magnetOrigin.position, magnetEffectRadius, magnetOrigin.forward,
                magnetEffectDistance);

            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider.CompareTag("Collectable"))
                {
                    raycastHits[i].collider.transform.position =
                        Vector3.MoveTowards(raycastHits[i].collider.transform.position, transform.position,
                            Time.deltaTime);
                }
            }
        }

        public void TurnOn()
        {
            _isActive = true;
        }

        public void TurnOff()
        {
            _isActive = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(magnetOrigin.position, magnetEffectRadius);
            Gizmos.DrawRay(magnetOrigin.position, magnetOrigin.forward * magnetEffectDistance);
        }
    }
}