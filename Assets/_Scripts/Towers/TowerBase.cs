using UnityEngine;

public class TowerBase : MonoBehaviour {
    public IAttackStrategy attackStrategy;
    public float range = 5f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    private void Update() {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f) {
            EnemyBase target = FindTarget();
            if (target != null) {
                attackStrategy?.Execute(this, target);
                fireCooldown = 1f / fireRate;
            }
        }
    }

    private EnemyBase FindTarget() {
        // Dummy target logic — sau này quét enemy trong tầm
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits) {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null) {
                return enemy;
            }
        }
        return null;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
