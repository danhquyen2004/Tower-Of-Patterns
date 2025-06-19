// Assets/_Scripts/Enemies/EnemyTorch.cs

using UnityEngine;

public class EnemyTorch : EnemyBase
{
    [SerializeField] private ParticleSystem burnEffect;
    private Transform lastBurnedTower = null;

    protected void Awake()
    {
        attackStrategy = new BurnAttackStrategy(burnEffect);
    }

    protected void Start()
    {
        base.Start();
        ChangeState(new EnemyMoveState());
    }


    protected void Update()
    {
        base.Update();

        if (!(currentState is EnemyBurnState) &&
            CurrentTowerTarget != null &&
            Vector3.Distance(transform.position, GetTowerPosition()) <= attackRange &&
            CurrentTowerTarget != lastBurnedTower)
        {
            ChangeState(new EnemyBurnState());
        }

        if (currentState is EnemyBurnState burnState && burnState.HasBurned && burnState.IsDone)
        {
            // Lưu lại tower vừa burn xong
            lastBurnedTower = CurrentTowerTarget;
            CurrentTowerTarget = null;
            ChangeState(new EnemyMoveState());
        }
    }

    public override void DetectAndTargetTower()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, detectionRange, LayerMask.GetMask("Ignore Raycast"));
        float minDist = float.MaxValue;
        Transform nearestTower = null;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Tower"))
            {
                // Nếu tower đang bị cháy, bỏ qua
                var burnEffect = hit.GetComponentInChildren<ParticleSystem>();
                if (burnEffect != null && burnEffect.isPlaying)
                    continue;

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
}