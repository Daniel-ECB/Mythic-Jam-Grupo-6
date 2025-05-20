using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float[] _health = { 100f };
    public int scoreToAdd = 100;
    private int _currentHealthIndex = 0;
    public int HealthIndex;
    [SerializeField] private GameObject dropItem;
    public float CurrentHealth => _health[_currentHealthIndex];
    public float MaxHealth => _health.Length > 0 ? _health[_currentHealthIndex] : 0f;

    [SerializeField] private ParticleSystem particlePrefab;

    

    void Update()
    {
        if (HealthIndex != _currentHealthIndex)
        {
            HealthIndex = _currentHealthIndex;
        }

        CheckBoundaries();

        if (_health[_currentHealthIndex] <= 0)
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
                ParticleSystem spawnedParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            }
        }
        ;
        Destroy(gameObject);
    }

    

    private void CheckBoundaries()
    {
        if (transform.position.x < EnemyMovementManager.minX || transform.position.x > EnemyMovementManager.maxX ||
            transform.position.y < EnemyMovementManager.minY || transform.position.y > EnemyMovementManager.maxY)
        {
            Destroy(gameObject);
        }
    }
}


