using System.Collections;
using UnityEngine;

public class EnemyBurnState : IEnemyState
{
    private bool hasBurned = false;
    private EnemyBase enemy;
    private bool isDone = false;

    public void Enter(EnemyBase enemy)
    {
        this.enemy = enemy;
        hasBurned = false;
        isDone = false;
        // Optionally: set burn animation
        enemy.Animator.Play("Attack");
    }

    public void Update(EnemyBase enemy)
    {
        if (!hasBurned && enemy.CurrentTowerTarget != null)
        {
            this.enemy.StartCoroutine(Attack());
            hasBurned = true;
        }
    }

    public void Exit(EnemyBase enemy)
    {
        // Optionally: reset burn animation
    }

    public bool HasBurned => hasBurned;
    public bool IsDone => isDone;

    private IEnumerator Attack()
    {
        
        float time = 0.3333333f;
        yield return new WaitForSeconds(time);
        enemy.attackStrategy?.Attack(enemy);
        yield return new WaitForSeconds(0.5f - time);
        isDone = true;
    }
}