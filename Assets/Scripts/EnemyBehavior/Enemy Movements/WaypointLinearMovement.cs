using UnityEngine;

public class WaypointLinearMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float[] arrayX;
    [SerializeField] private float[] arrayY;
    [SerializeField] private float tolerance = 0.1f;

    private int currentIndex = 0;
    public EnemyMovementController enemyMovementScript { get; set; }

    void Start()
    {
        if (enemyMovementScript == null)
        {
            enemyMovementScript = GetComponent<EnemyMovementController>();
        }
    }

    public void Move(Transform enemyTransform)
    {
        if (arrayX.Length == 0 || arrayY.Length == 0) return;

        int length = Mathf.Min(arrayX.Length, arrayY.Length);
        if (length == 0 || currentIndex >= length) return;

        Vector3 targetPosition = new Vector3(arrayX[currentIndex], arrayY[currentIndex], enemyTransform.position.z);
        float speed = enemyMovementScript != null ? enemyMovementScript.Speed : 1f;

        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(enemyTransform.position, targetPosition) < tolerance)
        {
            currentIndex++;
            if (currentIndex >= length)
            {
                currentIndex = 0; 
            }
        }
    }
}


