using UnityEngine;

public class BulletBase : MonoBehaviour {
    public float damage = 10f;
    public IBulletEffect effect;

    private void OnTriggerEnter(Collider other) {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
            effect?.Apply(enemy);
            Destroy(gameObject);
        }
    }
}
