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
        if (currentTowerTarget != null)
        {
            MoveTowards(currentTowerTarget.position);

            if (Vector3.Distance(transform.position, currentTowerTarget.position) <= attackRange)
            {
                AttackTower();
            }
        }
        else
        {
            DetectAndTargetTower();

            if (currentTowerTarget == null)
            {
                FollowPath();
            }
        }
    }

    protected virtual void MoveTowards(Vector3 targetPos)
    {
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
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, LayerMask.GetMask("Tower"));
        float minDist = float.MaxValue;
        Transform nearestTower = null;

        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestTower = hit.transform;
            }
        }

        if (nearestTower != null)
        {
            currentTowerTarget = nearestTower;
        }
    }

    protected virtual void AttackTower()
    {
        // Có thể gọi tower.TakeDamage nếu tower có máu
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

}
