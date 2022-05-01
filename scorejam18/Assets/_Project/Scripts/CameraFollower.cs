using UnityEngine;

namespace Gisha.scorejam18
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float followSpeed;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            Vector3 newPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}