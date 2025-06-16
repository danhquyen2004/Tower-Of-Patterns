using UnityEngine;

public class MeleeAttackStrategy : IEnemyAttackStrategy {
    public void Attack(EnemyBase enemy) {
        GameManager.Instance.TakeBaseDamage(10);
        Debug.Log($"{enemy.name} đánh base gây 10 damage");
        enemy.Die(); // Ví dụ: tự hủy sau khi đánh (Barrel)
    }
}

