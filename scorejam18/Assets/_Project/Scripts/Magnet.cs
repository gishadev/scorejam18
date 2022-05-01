using System.Collections.Generic;
using UnityEngine;

namespace Gisha.scorejam18
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private Transform magnetOrigin;
        [SerializeField] private float magnetEffectDistance;
        [SerializeField] private float magnetEffectRadius;
        [SerializeField] private float breakSqrRadius;

        [SerializeField] private float magnetPower;
        [SerializeField] private float pushForce;


        private Rigidbody _capturedRb;
        private bool _isActive;

        public bool IsActive => _isActive;

        private void Update()
        {
            if (!IsActive || _capturedRb != null)
                return;

            var raycastHits = Physics.SphereCastAll(magnetOrigin.position, magnetEffectRadius, magnetOrigin.forward,
                magnetEffectDistance);

            for (int i = 0; i < raycastHits.Length; i++)
            {
                if (raycastHits[i].collider.CompareTag("Collectable"))
                {
                    var rb = raycastHits[i].collider.GetComponent<Rigidbody>();
                    var position =
                        Vector3.MoveTowards(raycastHits[i].collider.transform.position, transform.position,
                            magnetPower * Time.deltaTime);
                    rb.MovePosition(position);


                    if ((raycastHits[i].collider.transform.position - magnetOrigin.transform.position).sqrMagnitude <
                        breakSqrRadius)
                    {
                        _capturedRb = rb;
                        _capturedRb.transform.SetParent(transform);
                    }
                }
            }
        }

        public void Magnetize()
        {
            _isActive = true;
        }

        public void Push()
        {
            _isActive = false;

            if (_capturedRb != null)
            {
                _capturedRb.transform.SetParent(null);
                _capturedRb.AddForce(magnetOrigin.forward * pushForce, ForceMode.Impulse);
                _capturedRb = null;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(magnetOrigin.position, magnetEffectRadius);
            Gizmos.DrawRay(magnetOrigin.position, magnetOrigin.forward * magnetEffectDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(magnetOrigin.position, Mathf.Sqrt(breakSqrRadius));
        }
    }
}