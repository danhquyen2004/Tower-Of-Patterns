using UnityEngine;

public interface IAttackStrategy {
    void Execute(TowerBase towerBase, EnemyBase target);
}
