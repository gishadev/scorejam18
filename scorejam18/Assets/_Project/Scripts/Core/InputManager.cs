using System;
using UnityEngine;

namespace Gisha.scorejam18.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [SerializeField] private Joystick movementJoystick;
        [SerializeField] private Joystick armJoystick;

        [SerializeField] private LayerMask groundMask;

        public static Action PointerDown;
        public static Action PointerUp;
        
        private void Awake()
        {
            Instance = this;

#if UNITY_ANDROID
            movementJoystick.gameObject.SetActive(true);
            armJoystick.gameObject.SetActive(true);
#endif
#if !UNITY_ANDROID
            movementJoystick.gameObject.SetActive(false);
            armJoystick.gameObject.SetActive(false);
#endif
        }

#if UNITY_ANDROID
        private void OnEnable()
        {
            armJoystick.OnJoystickPointerDown += OnJoystickPointerDown;
            armJoystick.OnJoystickPointerUp += OnJoystickPointerUp;
        }
        
        private void OnDisable()
        {
            armJoystick.OnJoystickPointerDown -= OnJoystickPointerDown;
            armJoystick.OnJoystickPointerUp -= OnJoystickPointerUp;
        }
#endif


#if !UNITY_ANDROID
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                PointerDown?.Invoke();
            if (Input.GetMouseButtonUp(0))
                PointerUp?.Invoke();
        }
#endif

#if UNITY_ANDROID
        private void OnJoystickPointerDown()
        {
            PointerDown?.Invoke();
        }
        
        private void OnJoystickPointerUp()
        {
            PointerUp?.Invoke();
        }
#endif

        public static Vector2 GetMovementDirection()
        {
#if UNITY_ANDROID
            return Instance.movementJoystick.Direction;
#endif

#if !UNITY_ANDROID
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
        }

        public static Vector2 GetArmDirection(Transform bodyOrigin)
        {
#if UNITY_ANDROID
            return Instance.armJoystick.Direction;
#endif

#if !UNITY_ANDROID
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, 100f, Instance.groundMask))
                return new Vector2(hitInfo.point.x - bodyOrigin.position.x, hitInfo.point.z - bodyOrigin.position.z)
                    .normalized;

            return Vector2.up;
#endif
        }
    }
}