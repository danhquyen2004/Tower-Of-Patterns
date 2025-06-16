using UnityEngine;

public class RangedAttackStrategy : IEnemyAttackStrategy {
    public void Attack(EnemyBase enemy) {
        // Ném bomb, hoặc bắn đạn về phía tháp (chưa triển khai)
        Debug.Log($"{enemy.name} ném bomb");
    }
}

