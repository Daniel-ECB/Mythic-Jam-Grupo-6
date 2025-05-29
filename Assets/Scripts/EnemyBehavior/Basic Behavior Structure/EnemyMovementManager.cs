using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] public static float minX = -10f;
    [SerializeField] public static float maxX = 10f;
    [SerializeField] public static float minY = -10f;
    [SerializeField] public static float maxY = 10f;
    [SerializeField] private float speed = 2f;
    public float Speed => speed;
    [SerializeField] private float delayBeforeMove;
    [SerializeField] private bool isMoving;
    private float delayTimer = 0f;
    private bool canMove = false;
    private Vector3 lastPosition;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    void Start()
    {
        lastPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
        if (animator == null)
        {
            Debug.LogWarning("You forgot the Animator in the EnemyMovementManager");
        }
    }
    void Update()
    {
        delayTimer += Time.deltaTime;
        if (!canMove && delayTimer >= delayBeforeMove)
        {
            canMove = true;
            foreach (Collider2D collider in GetComponents<Collider2D>())
            {
                collider.enabled = true;
            }
        }

        if (canMove)
        {
            foreach (IEnemyMovement movement in GetComponents<IEnemyMovement>())
            {
                movement.Move(transform);
            }
        }

        Vector3 deltaPosition = transform.position - lastPosition;
        isMoving = (deltaPosition != Vector3.zero);

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = deltaPosition.x > 0;
        }

        lastPosition = transform.position;

        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}

