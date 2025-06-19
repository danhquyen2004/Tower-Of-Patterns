using UnityEngine;

public class BombAttackStrategy : IEnemyAttackStrategy
{
    public void Attack(EnemyBase enemy)
    {
        TowerBase targetTower = enemy.CurrentTowerTarget.GetComponent<TowerBase>();
        if (targetTower != null)
        {
            targetTower.TakeDamage(enemy.attackDamage);
        }
    }
}