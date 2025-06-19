using UnityEngine;

public class EnemyMoveState : IEnemyState
{
    private bool animationPlay = false;
    public void Enter(EnemyBase enemy)
    {
        animationPlay = false;
        
    }

    public void Update(EnemyBase enemy)
    {
        if (animationPlay == false)
        {
            enemy.Animator.Play("Move");
            animationPlay = true;
        }
        
        ChangeFaceDirection(enemy);
        
        if (enemy is EnemyTorch torch)
        {
            torch.DetectAndTargetTower();
        }
        else
            enemy.DetectAndTargetTower();

        if (enemy.CurrentTowerTarget != null)
        {
            Debug.Log("EnemyMoveState: Moving towards tower");
            enemy.SetMoveTarget(enemy.GetTowerPosition());
        }
        else if (enemy.baseTarget != null)
        {
            Debug.Log("EnemyMoveState: Moving towards base");
            enemy.SetMoveTarget(enemy.baseTarget.position);
        }
    }

    public void Exit(EnemyBase enemy)
    {
        // Optionally: reset move animation
    }
    
    private void ChangeFaceDirection(EnemyBase enemy)
    {
        Vector3 moveDir = enemy.AIPath.desiredVelocity.normalized;

        // Nếu đang di chuyển (tránh trường hợp đứng yên)
        if (moveDir != Vector3.zero)
        {
            if (moveDir.x > 0)
            {
                enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (moveDir.x < 0)
            {
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}