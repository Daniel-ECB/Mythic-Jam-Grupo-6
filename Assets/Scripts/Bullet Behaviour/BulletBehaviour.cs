using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifeTime)
            gameObject.SetActive(false);
    }
}
