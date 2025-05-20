
public interface IEnemyAttack 
{
    void Attack();
    int[] HealthIndexes { get; }
    EnemyAttackManager enemyAttackManager { get; set; }

    //Tutorial for the guys just starting. Just make the Attack Script you want to create inherit from this interface (put a comma where the MonoBehavior
    //script is referenced and write this script's name there. You need to implement the methods defined here, such as Attack, and it must access the EnemyAttackManager)

    //Refer to Ignacio Rodriguez a.k.a IgnaZ10 on Discord if you have any doubts. The HealthIndexes are put as a reference just in case we want to make
    //multi-phased bosses later.
}
