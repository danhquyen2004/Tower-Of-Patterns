// Assets/_Scripts/Enemies/States/EnemyAttackState.cs

using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attackCooldown = 1f;
    private float lastAttackTime = -Mathf.Infinity;

    public void Enter(EnemyBase enemy)
    {
        // Optionally: set animation to attack
    }

    public void Update(EnemyBase enemy)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            enemy.attackStrategy?.Attack(enemy);
            lastAttackTime = Time.time;
        }
    }

    public void Exit(EnemyBase enemy)
    {
        // Optionally: reset attack animation
    }
}