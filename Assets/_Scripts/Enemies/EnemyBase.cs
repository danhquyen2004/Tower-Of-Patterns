using UnityEngine;

public enum EnemyStateType {
    Move,
    Attack,
    Dead
}

public class EnemyBase : MonoBehaviour {
    public float health = 100;
    public float speed = 2f;
    public IEnemyAttackStrategy attackStrategy;

    private EnemyStateType currentState = EnemyStateType.Move;
    private Transform targetPoint; // Điểm cuối đường đi (base)

    private void Start() {
        targetPoint = GameObject.FindWithTag("Base").transform;
    }

    private void Update() {
        switch (currentState) {
            case EnemyStateType.Move:
                Move();
                break;
            case EnemyStateType.Attack:
                attackStrategy?.Attack(this);
                break;
            case EnemyStateType.Dead:
                break;
        }
    }

    private void Move() {
        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f) {
            currentState = EnemyStateType.Attack;
        }
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0 && currentState != EnemyStateType.Dead) {
            Die();
        }
    }

    public void Die() {
        currentState = EnemyStateType.Dead;
        GoldManager.Instance.AddGold(10);
        Destroy(gameObject); // Hoặc Pooling
    }
}
