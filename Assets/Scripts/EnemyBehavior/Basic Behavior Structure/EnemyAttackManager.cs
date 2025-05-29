using System;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private float secondsBetweenShots = 0.5f;
    public bool canAttack = false;
    private float attackTimer = 0f;
    private Renderer enemyRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip[] attackAnimations;
    [SerializeField] private AnimationClip loopAttackAnimation;
    private int currentAttackIndex = 0;
    private bool isAttacking = false;
    public Vector2 LastMovementDirection { get; set; }

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        if (animator == null)
        {
            Debug.LogWarning("Hey, you forgot the animator in EnemyAttackManager.");
        }
    }
    void Update()
    {
        if (canAttack && Time.timeScale != 0 && IsInCameraBounds() && !isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= secondsBetweenShots)
            {
                StartAttack();
                attackTimer = 0f;
            }
        }
    }
    private void StartAttack()
    {
        isAttacking = true;
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
            PlayNextAttackAnimation();
        }

        foreach (IEnemyAttack attack in GetComponents<IEnemyAttack>())
        {
            if (Array.Exists(attack.HealthIndexes, index => index == GetComponent<Enemy>().HealthIndex))
            {
                attack.Attack();
            }
        }
        Invoke("StopAttack", GetCurrentAnimationLength());
    }
    private void PlayNextAttackAnimation()
    {
        if (currentAttackIndex < attackAnimations.Length)
        {
            animator.Play(attackAnimations[currentAttackIndex].name);
            currentAttackIndex++;
        }
        else
        {
            animator.Play(loopAttackAnimation.name);
        }
    }
    private float GetCurrentAnimationLength()
    {
        if (currentAttackIndex - 1 < attackAnimations.Length)
        {
            return attackAnimations[currentAttackIndex - 1].length;
        }
        return loopAttackAnimation.length;
    }
    private void StopAttack()
    {
        isAttacking = false;
        currentAttackIndex = 0;
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }
    private bool IsInCameraBounds()
    {
        return enemyRenderer != null && enemyRenderer.isVisible;
    }
}



