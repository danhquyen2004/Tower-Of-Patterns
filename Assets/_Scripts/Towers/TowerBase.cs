using System;
using UnityEngine;
using CodeMonkey.HealthSystemCM;

public class TowerBase : MonoBehaviour, IGetHealthSystem
{
    public IAttackStrategy attackStrategy;
    public float maxHealth = 100f;
    public float currentHealth;
    public float range = 5f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    public float speed = 4f;
    public float damage = 10f; // Sát thương cơ bản
    public int level = 1;
    public SpriteRenderer sprite;
    
    public ArcherController archerController;
    private TowerSpot towerSpot;
    
    private HealthSystem healthSystem;

    public virtual void Initialize()
    {
        towerSpot = GetComponentInParent<TowerSpot>();
        healthSystem = new HealthSystem(maxHealth);
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            EnemyBase target = FindTarget();
            if (target != null)
            {
                archerController.Shoot();
                //attackStrategy?.Execute(this, target);
                fireCooldown = 1f / fireRate;
            }
            else
            {
                archerController.StopAnimation();
            }
        }
    }

    public void Upgrade()
    {
        level++;
        damage *= 1.5f;
        fireRate *= 1.2f;
        archerController.UpdateAnimationSpeed(fireRate); // Cập nhật tốc độ animation
        Debug.Log($"{name} đã nâng cấp lên cấp {level}");
    }

    public EnemyBase FindTarget()
    {
        // Dummy target logic — sau này quét enemy trong tầm
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null) { 
                return enemy;
            }
        }
        return null;
    }
    
    public void TakeDamage(float damage)
    {
        healthSystem.Damage(damage);
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Debug.Log($"{name} đã chết");
        // Xử lý khi tower chết, ví dụ: hủy đối tượng
        towerSpot.DestroyTower();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
