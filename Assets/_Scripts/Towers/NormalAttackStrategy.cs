using UnityEngine;

public class NormalAttackStrategy : IAttackStrategy {
    public void Execute(TowerBase tower, EnemyBase target) {
        target.TakeDamage(10);  // Damage mẫu
        Debug.Log($"{tower.name} bắn {target.name} gây 10 damage");
    }
}
