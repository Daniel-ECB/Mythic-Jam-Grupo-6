using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;

    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnEnable()
    {
        rb.linearVelocity = direction * speed;

        
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
        CancelInvoke();
    }

    private void ReturnToPool()
    {
        BulletPool.instance.ReturnBullet(gameObject);
    }
}

