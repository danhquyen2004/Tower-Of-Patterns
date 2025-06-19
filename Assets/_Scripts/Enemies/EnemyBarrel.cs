using UnityEngine;
public class EnemyBarrel : EnemyBase
{
    protected void Awake()
    {
        attackStrategy = new BombAttackStrategy();
    }
    protected void Start()
    {
        base.Start();
        ChangeState(new EnemyMoveState());
    }


    protected void Update()
    {
        base.Update();
        if (!(currentState is EnemySuicideState) &&
            CurrentTowerTarget != null &&
            Vector3.Distance(transform.position, GetTowerPosition()) <= attackRange)
        {
            ChangeState(new EnemySuicideState(CurrentTowerTarget));
        }
        
    }
}