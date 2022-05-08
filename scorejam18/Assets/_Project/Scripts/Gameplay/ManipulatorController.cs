using Gisha.Effects.Audio;
using Gisha.scorejam18.Core;
using UnityEngine;

namespace Gisha.scorejam18.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class ManipulatorController : MonoBehaviour
    {
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

        private void OnEnable()
        {
            InputManager.PointerDown += Magnetize;
            InputManager.PointerUp += MagnetPush;
        }
        
        private void OnDisable()
        {
            InputManager.PointerDown -= Magnetize;
            InputManager.PointerUp -= MagnetPush;
        }

        private void Update()
        {
            GetInput();

            ArmRotate();
            BodyRotate();

            if (Mathf.Abs(_moveHInput) > 0 || Mathf.Abs(_moveVInput) > 0)
                AudioManager.Instance.PlaySFX("engine");
            else
                AudioManager.Instance.StopSFX("engine");
        }

        private void FixedUpdate()
        {
            BodyMove();
        }

        private void Magnetize()
        {
            _magnet.Magnetize();

            AudioManager.Instance.PlaySFX("magnet");
        }

        private void MagnetPush()
        {
            _magnet.Push();

            AudioManager.Instance.StopSFX("magnet");
            AudioManager.Instance.PlaySFX("push");
        }

        private void GetInput()
        {
            _moveVInput = InputManager.GetMovementDirection().y;
            _moveHInput = InputManager.GetMovementDirection().x;

            _armVInput = InputManager.GetArmDirection(transform).y;
            _armHInput = InputManager.GetArmDirection(transform).x;
        }

        private void BodyMove()
        {
#if !UNITY_ANDROID
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 0f && Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0f)
            {
                _rb.velocity = Vector3.zero;
                return;
            }
#endif

            var moveDir = new Vector3(_moveHInput, 0f, _moveVInput).normalized;
            _rb.velocity = moveDir * moveSpeed;
        }

        private void ArmRotate()
        {
#if UNITY_ANDROID
            if (Mathf.Abs(_armHInput) <= 0f || Mathf.Abs(_armVInput) <= 0f)
                return;
#endif

            var dir = new Vector3(_armHInput, 0f, _armVInput).normalized;
            armRig.rotation = Quaternion.LookRotation(dir);
        }

        private void BodyRotate()
        {
#if UNITY_ANDROID
            if (Mathf.Abs(_moveHInput) <= 0f || Mathf.Abs(_moveVInput) <= 0f)
                return;
#endif

#if !UNITY_ANDROID
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) <= 0f && Mathf.Abs(Input.GetAxisRaw("Vertical")) <= 0f)
                return;
#endif

            var dir = new Vector3(_moveHInput, 0f, _moveVInput).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}