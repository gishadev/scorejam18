using UnityEngine;

namespace Gisha.scorejam18
{
    [RequireComponent(typeof(Rigidbody))]
    public class ManipulatorMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        
        [SerializeField] private float moveSpeed;
        
        
        private Vector3 _moveInput;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _moveInput = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
        }

        private void FixedUpdate()
        {
            _rb.velocity = _moveInput * moveSpeed;
        }
    }
}