using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace MythicGameJam.Input
{
    public sealed class InputManager : Core.Utils.Singleton<InputManager>
    {
        private InputSystem_Actions _inputActions;

        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnLook;
        public event Action OnAttack;

        public InputSystem_Actions InputActions => _inputActions;

        protected override void Awake()
        {
            base.Awake();
            _inputActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Player.Move.performed += HandleMove;
            _inputActions.Player.Move.canceled += HandleMove;

            _inputActions.Player.Look.performed += HandleLook;
            _inputActions.Player.Look.canceled += HandleLook;

            _inputActions.Player.Attack.performed += HandleAttack;
        }

        private void OnDisable()
        {
            if (_inputActions == null)
                return;

            _inputActions.Player.Move.performed -= HandleMove;
            _inputActions.Player.Move.canceled -= HandleMove;

            _inputActions.Player.Look.performed -= HandleLook;
            _inputActions.Player.Look.canceled -= HandleLook;

            _inputActions.Player.Attack.performed -= HandleAttack;

            _inputActions.Disable();
        }

        private void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            OnMove?.Invoke(move);
        }

        private void HandleLook(InputAction.CallbackContext context)
        {
            Vector2 look = context.ReadValue<Vector2>();
            OnLook?.Invoke(look);
        }

        private void HandleAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttack?.Invoke();
        }
    }
}
