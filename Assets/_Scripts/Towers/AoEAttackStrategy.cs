using UnityEngine;

public class AoEAttackStrategy : IAttackStrategy {
    public void Execute(TowerBase tower, EnemyBase target) {
        Collider[] hits = Physics.OverlapSphere(target.transform.position, 2f);
        foreach (var hit in hits) {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null) {
                enemy.TakeDamage(5);
                Debug.Log($"{tower.name} AoE bắn {enemy.name} gây 5 damage");
            }
        }
    }
}
