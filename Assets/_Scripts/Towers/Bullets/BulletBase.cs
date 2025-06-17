using UnityEngine;

public class BulletBase : MonoBehaviour {
    public float damage = 10f;
    public float speed = 10f;
    EnemyBase target;
    public IBulletEffect effect;
    
    public void Initialize(float damage, float speed, IBulletEffect effect, EnemyBase target = null) {
        this.damage = damage;
        this.speed = speed;
        this.effect = effect;
        this.target = target;
    }
    
    private void Update() {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
            effect?.Apply(enemy);
            Destroy(gameObject);
        }
    }
}
