using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    [SerializeField] private float[] _initialHealth = { 100f }; 
    private float[] _health;
    public float CurrentHealth =>
        _currentHealthIndex < _health.Length ? _health[_currentHealthIndex] : 0f;
    // This is here to prevent OOB index range access during execution time, since it used to go OOB once the last health bar depletes. This is probably a hack fix.

    private int _currentHealthIndex = 0;
    public int HealthIndex;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private ParticleSystem particlePrefab;
    void OnEnable()
    {
        ResetEnemy(); 
    }
    void Update()
    {
        if (HealthIndex != _currentHealthIndex)
        {
            HealthIndex = _currentHealthIndex;
        }

        CheckBoundaries();

        if (_currentHealthIndex < _health.Length && _health[_currentHealthIndex] <= 0)
        {
            _currentHealthIndex++;
            if (_currentHealthIndex >= _health.Length)
            {
                Die();
            }
        }
    }
    public void TakeDamage(float damage)
    {
        if (_currentHealthIndex < _health.Length)
        {
            _health[_currentHealthIndex] -= damage;

            if (_health[_currentHealthIndex] <= 0)
            {
                _currentHealthIndex++;
                if (_currentHealthIndex >= _health.Length)
                {
                    Die();
                }
            }
        }
    }
    private void Die()
    {
        bool allHealthEmpty = true;
        foreach (float healthValue in _health)
        {
            if (healthValue > 0f)
            {
                allHealthEmpty = false;
                break;
            }
        }

        if (allHealthEmpty)
        {
            if (dropItem != null)
            {
                Instantiate(dropItem, transform.position, Quaternion.identity);
            }

            if (particlePrefab != null)
            {
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }

            gameObject.SetActive(false); // Recycle the enemy instead of destroying it
        }
    }
    public void ResetEnemy()
    {
        _currentHealthIndex = 0;
        _health = new float[_initialHealth.Length];
        for (int i = 0; i < _initialHealth.Length; i++)
        {
            _health[i] = _initialHealth[i];
        }
        //This parametrizes health so it automatically resets to the one it had at the beginning once it respawns.
    }
    private void CheckBoundaries()
    {
        if (transform.position.x < EnemyMovementController.minX || transform.position.x > EnemyMovementController.maxX ||
            transform.position.y < EnemyMovementController.minY || transform.position.y > EnemyMovementController.maxY)
        {
            gameObject.SetActive(false); // Use pooling instead of destroying when it leaves the limit.
        }
    }
}






