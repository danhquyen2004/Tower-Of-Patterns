using UnityEngine;

public class BurnAttackStrategy : IEnemyAttackStrategy
{
    public void Attack(EnemyBase enemy)
    {
        // Nếu có tower đang nhắm tới
        if (enemy.CurrentTowerTarget != null)
        {
            Debug.Log($"{enemy.name} đốt tower {enemy.CurrentTowerTarget.name}");

            // TODO: sau này nên gọi Tower.TakeDamage(damage) nếu Tower có máu
            GameObject.Destroy(enemy.CurrentTowerTarget.gameObject);

            // Sau khi tấn công xong, đặt lại mục tiêu
            enemy.CurrentTowerTarget = null;
        }
        else
        {
            // Không có tower nào trong tầm → tấn công base
            GameManager.Instance.TakeBaseDamage(5);
            Debug.Log($"{enemy.name} đốt base gây 5 damage");
        }

        // Không tự chết sau khi đánh như Barrel
    }
}
