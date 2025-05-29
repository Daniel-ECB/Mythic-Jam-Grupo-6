using UnityEngine;

public class WaypointLinearMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float[] arrayX;
    [SerializeField] private float[] arrayY;
    private int currentIndex = 0;

    public EnemyMovementController enemyMovementScript { get; set; }

    private void Start()
    {
        if (arrayX.Length != arrayY.Length)
        {
            Debug.LogWarning("WaypointLinearMovement: arrayX and arrayY must be of the same length.");
        }
    }

    public void Move(Transform enemyTransform)
    {
        if (arrayX.Length == 0 || arrayY.Length == 0 || currentIndex >= arrayX.Length)
            return;

        Vector3 target = new Vector3(arrayX[currentIndex], arrayY[currentIndex], enemyTransform.position.z);
        Vector3 direction = (target - enemyTransform.position).normalized;

        float step = enemyMovementScript != null ? enemyMovementScript.Speed * Time.deltaTime : 0f;
        enemyTransform.position += direction * step;

        if (Vector3.Distance(enemyTransform.position, target) < 0.1f)
        {
            currentIndex++;
            if (currentIndex >= arrayX.Length)
            {
                currentIndex = 0; 
            }
        }
    }
}

