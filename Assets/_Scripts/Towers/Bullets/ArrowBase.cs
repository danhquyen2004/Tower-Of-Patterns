using System;
using UnityEngine;

public abstract class ArrowBase : MonoBehaviour {
    protected TowerBase tower;
    protected EnemyBase target;
    protected IBulletEffect effect;
    
    public void Initialize(TowerBase tower, EnemyBase target = null) {
        this.tower = tower;
        this.target = target;
    }
    
    private void Update() {
        if (target != null) {
            // Tính toán hướng từ mũi tên đến mục tiêu
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Tính toán góc xoay để mũi tên hướng về mục tiêu
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Di chuyển mũi tên về phía mục tiêu
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, tower.speed * Time.deltaTime);
        } else {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null) {
            Debug.Log("Bullet hit: " + enemy.name);
            enemy.TakeDamage(tower.damage);
            effect?.Apply(enemy);
            //Destroy(gameObject);
            gameObject.SetActive(false); // Thay vì Destroy, dùng Object Pooling
        }
    }
}
