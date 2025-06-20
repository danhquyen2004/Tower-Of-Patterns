using System.Collections;
using UnityEngine;

public class EnemySuicideState : IEnemyState
{
    private EnemyBase enemy;
    private bool hasExploded = false;
    private bool isDone = false;
    private Coroutine explodeCoroutine;
    private Transform target;

    public EnemySuicideState(Transform target)
    {
        this.target = target;
    }

    public void Enter(EnemyBase enemy)
    {
        this.enemy = enemy;
        hasExploded = false;
        isDone = false;

        // Play animation nổ cảm tử nếu có
        if (enemy.Animator != null)
        {
            enemy.Animator.Play("Attack");
        }
    }

    public void Update(EnemyBase enemy)
    {
        if (!hasExploded && target != null)
        {
            explodeCoroutine = enemy.StartCoroutine(Explode());
            hasExploded = true;
        }
    }

    public void Exit(EnemyBase enemy)
    {
        
    }

    public bool HasExploded => hasExploded;
    public bool IsDone => isDone;

    private IEnumerator Explode()
    {
        float time = 0.3333333f;
        // Đợi đồng bộ với animation nếu cần (ví dụ 0.33s)
        yield return new WaitForSeconds(time);
        enemy.attackStrategy?.Attack(enemy);

        // Đợi thêm một chút cho animation nổ hoàn thành
        yield return new WaitForSeconds(0.5f-time);

        enemy.Die();

        isDone = true;
    }
}