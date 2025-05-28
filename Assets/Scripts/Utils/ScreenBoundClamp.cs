using UnityEngine;

namespace MythicGameJam.Core.Utils
{
    public sealed class ScreenBoundClamp : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _margin = new Vector2(0.5f, 0.5f);

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            if (_camera == null) return;

            Vector3 pos = transform.position;

            Vector3 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

            float minX = bottomLeft.x + _margin.x;
            float maxX = topRight.x - _margin.x;
            float minY = bottomLeft.y + _margin.y;
            float maxY = topRight.y - _margin.y;

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;
        }
    }
}
