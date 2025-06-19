using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float maxHealth = 100f;
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public int goldReward = 10;
    public IEnemyAttackStrategy attackStrategy;

    [HideInInspector] public bool isAttack = false;
    public Transform CurrentTowerTarget;
    [HideInInspector] public Transform baseTarget;
    
    protected float currentHealth;
    protected IEnemyState currentState;

    // A* Pathfinding Project
    private AIPath aiPath;
    public AIPath AIPath => aiPath;
    
    private Animator animator;
    public Animator Animator => animator;
    
    protected virtual void Start()
    {
        baseTarget = GameObject.FindWithTag("Base")?.transform;
        animator = GetComponentInChildren<Animator>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = speed;

        // Đặt điểm đích ban đầu là base
        if (baseTarget != null) aiPath.destination = baseTarget.position;
        
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        currentState?.Update(this);
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState?.Enter(this);
    }

    public virtual void DetectAndTargetTower()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange,LayerMask.GetMask("Ignore Raycast"));
        float minDist = float.MaxValue;
        Transform nearestTower = null;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tower"))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestTower = hit.transform;
                }
            }
        }
        CurrentTowerTarget = nearestTower;
    }

    // Gọi hàm này để đổi mục tiêu di chuyển
    public void SetMoveTarget(Vector3 worldTarget)
    {
        if (aiPath != null)
            aiPath.destination = worldTarget;
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            ChangeState(new EnemyDieState());
        }
    }

    public virtual void Die()
    {
        // Reward player, play effects, etc.
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public Vector3 GetTowerPosition()
    {
        if (CurrentTowerTarget != null)
        {
            BoxCollider2D col = CurrentTowerTarget.GetComponent<BoxCollider2D>();
            Vector3 bottom = col.bounds.center + Vector3.down * (col.bounds.size.y / 2f);
            return bottom;
        }
        return Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CastleController castle = other.GetComponent<CastleController>();
        if (castle != null)
        {
            castle.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
    }

    #region Animation

    private Coroutine animationCoroutine;

    /// <summary>
    /// Chuyển đổi animation theo tên, đảm bảo animation cũ chạy hết mới chuyển qua animation mới.
    /// </summary>
    public void PlayAnimationAfterCurrent(string nextAnimName)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(PlayAnimationAfterCurrentCoroutine(nextAnimName));
    }

    private IEnumerator PlayAnimationAfterCurrentCoroutine(string nextAnimName)
    {
        // Lấy state hiện tại
        AnimatorStateInfo currentState = Animator.GetCurrentAnimatorStateInfo(0);
        float timeLeft = (1f - currentState.normalizedTime) * currentState.length;

        // Nếu animation đang chạy, chờ cho nó chạy hết
        if (timeLeft > 0f)
        {
            yield return new WaitForSeconds(timeLeft);
        }

        Animator.Play(nextAnimName, 0);
    }

    #endregion
}