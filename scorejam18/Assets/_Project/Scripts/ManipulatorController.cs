using UnityEngine;

namespace Gisha.scorejam18
{
    [RequireComponent(typeof(Rigidbody))]
    public class ManipulatorController : MonoBehaviour
    {
        [Header("Joysticks")] [SerializeField] private Joystick movementJoystick;
        [SerializeField] private Joystick armJoystick;

        [Header("General")] [SerializeField] private float moveSpeed;
        [SerializeField] private Transform armRig;

        private float _armVInput, _armHInput;
        private float _moveVInput, _moveHInput;
        private Magnet _magnet;
        private Rigidbody _rb;

        private void Awake()
        {
            _magnet = GetComponentInChildren<Magnet>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            GetInput();

            ArmRotate();
            BodyRotate();
        }

        private void FixedUpdate()
        {
            BodyMove();
        }

        public void Magnetize()
        {
            if (!_magnet.IsActive)
                _magnet.TurnOn();
            else
                _magnet.TurnOff();
        }

        private void GetInput()
        {
            _moveVInput = movementJoystick.Vertical;
            _moveHInput = movementJoystick.Horizontal;

            _armVInput = armJoystick.Vertical;
            _armHInput = armJoystick.Horizontal;
        }

        private void BodyMove()
        {
            var moveDir = new Vector3(_moveHInput, 0f, _moveVInput).normalized;
            _rb.velocity = moveDir * moveSpeed;
        }

        private void ArmRotate()
        {
            if (Mathf.Abs(armJoystick.Horizontal) <= 0f || Mathf.Abs(armJoystick.Vertical) <= 0f)
                return;

            var dir = new Vector3(_armHInput, 0f, _armVInput).normalized;
            armRig.rotation = Quaternion.LookRotation(dir);
        }

        private void BodyRotate()
        {
            if (Mathf.Abs(movementJoystick.Horizontal) <= 0f || Mathf.Abs(movementJoystick.Vertical) <= 0f)
                return;

            var dir = new Vector3(_moveHInput, 0f, _moveVInput).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}