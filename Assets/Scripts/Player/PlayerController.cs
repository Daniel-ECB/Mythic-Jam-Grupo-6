using UnityEngine;
using MythicGameJam.Input;

namespace MythicGameJam.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStats _stats;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Vector2 _moveInput;
        private Camera _mainCamera;

        private void Awake()
        {
            if (_spriteRenderer && _stats)
                _spriteRenderer.sprite = _stats.playerSprite;

            _rigidbody2D.linearDamping = _stats.linearDamping;
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            InputManager.Instance.OnMove += HandleMove;
            InputManager.Instance.OnLook += HandleLook;
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.OnMove -= HandleMove;
                InputManager.Instance.OnLook -= HandleLook;
            }
        }

        private void FixedUpdate()
        {
            // Movement logic
            if (_moveInput != Vector2.zero)
                _rigidbody2D.AddForce(_moveInput * _stats.acceleration);

            if (_rigidbody2D.linearVelocity.magnitude > _stats.moveSpeed)
                _rigidbody2D.linearVelocity = _rigidbody2D.linearVelocity.normalized * _stats.moveSpeed;
        }

        private void HandleMove(Vector2 move)
        {
            _moveInput = move.normalized;
        }

        private void HandleLook(Vector2 mouseScreenPosition)
        {
            Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 direction = (mouseWorldPos - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
