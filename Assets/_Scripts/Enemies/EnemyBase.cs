using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHealth = 100f;
    public float speed = 2f;
    public float detectionRange = 5f;  // phạm vi phát hiện tower
    public float attackRange = 1f;     // phạm vi tấn công tower hoặc base
    [SerializeField] protected Transform parentOfWaypoints; // Parent object chứa các waypoint
    protected float currentHealth;
    protected Transform baseTarget;
    protected Transform currentTowerTarget = null;
    public IEnemyAttackStrategy attackStrategy;
    public bool isAttack = false; // Biến này có thể dùng để xác định xem enemy có đang tấn công hay không


    [SerializeField] protected List<Transform> pathWaypoints;
    protected int currentPathIndex = 0;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        baseTarget = GameObject.FindWithTag("Base").transform;
    }

    protected virtual void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        DetectAndTargetTower();

        if (currentTowerTarget != null)
        {
            MoveTowards(currentTowerTarget.position);

            if (Vector3.Distance(transform.position, currentTowerTarget.position) <= attackRange)
            {
                isAttack = true;
                attackStrategy?.Attack(this);
                //AttackTower(); // Tấn công tower
                // Sau khi tấn công xong, đặt lại mục tiêu
                currentTowerTarget = null;
                isAttack = false; // Đặt lại trạng thái tấn công
            }
        }
        else
        {
                FollowPath();
        }
    }

    protected virtual void MoveTowards(Vector3 targetPos)
    {
        if (isAttack) return; // Nếu đang tấn công thì không di chuyển
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    protected virtual void FollowPath()
    {
        if (currentPathIndex < pathWaypoints.Count)
        {
            Transform waypoint = pathWaypoints[currentPathIndex];
            MoveTowards(waypoint.position);

            if (Vector3.Distance(transform.position, waypoint.position) <= 0.1f)
            {
                currentPathIndex++;
            }
        }
        else
        {
            // Tới base
            if (Vector3.Distance(transform.position, baseTarget.position) <= attackRange)
            {
                attackStrategy?.Attack(this);
            }
            else
            {
                MoveTowards(baseTarget.position);
            }
        }
    }

    protected virtual void DetectAndTargetTower()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        float minDist = float.MaxValue;
        Transform nearestTower = null;

        foreach (var hit in hits)
        {
            TowerBase tower = hit.GetComponent<TowerBase>();
            if(tower != null)
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestTower = hit.transform;
                }
            }
        }

        if (nearestTower != null)
        {
            currentTowerTarget = nearestTower;
        }
    }

    protected virtual void AttackTower()
    {
        Destroy(currentTowerTarget.gameObject);
        currentTowerTarget = null;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        GoldManager.Instance.AddGold(10);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        // Vẽ detection range (màu vàng)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Vẽ attack range (màu đỏ)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    [Button]
    private void LoadWaypoints()
    {
        foreach (Transform child in parentOfWaypoints)
        {
            pathWaypoints.Add(child);
        }
    }

    public Transform CurrentTowerTarget
    {
        get => currentTowerTarget;
        set => currentTowerTarget = value;
    }


}
