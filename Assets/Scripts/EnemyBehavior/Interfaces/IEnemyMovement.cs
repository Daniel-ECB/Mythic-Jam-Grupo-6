
using UnityEngine;

public interface IEnemyMovement
{
    void Move(Transform enemyTransform);
    EnemyMovementController enemyMovementScript { get; set; }

    //Tutorial for the guys just starting. Just make the Movement Script you want to create inherit from this interface (put a comma where the MonoBehavior
    //script is referenced and write this script's name there. You need to implement the methods defined here, such as Move, and it must access the EnemyMovementController)

    //Refer to Ignacio Rodriguez a.k.a IgnaZ10 on Discord if you have any doubts.
}
