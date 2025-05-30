using UnityEngine;

namespace MythicGameJam.Enemies
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 3f;

        private Transform _targetWaypoint;

        public event System.Action<GameObject> OnEnemyDestroyed;

        private void Start()
        {
            RequestNewWaypoint();
        }

        private void Update()
        {
            if (_targetWaypoint == null) return;

            Vector2 direction = (_targetWaypoint.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, _targetWaypoint.position) < 0.1f)
            {
                RequestNewWaypoint();
            }
        }

        private void OnDestroy()
        {
            OnEnemyDestroyed?.Invoke(gameObject);
        }

        private void RequestNewWaypoint()
        {
            _targetWaypoint = EnemiesManager.Instance.GetRandomWaypoint();
        }
    }
}
