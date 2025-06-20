using System.Collections;
using UnityEngine;

/// <summary>
/// Đốt tower, gây damage theo thời gian và gắn hiệu ứng cháy.
/// </summary>
public class BurnAttackStrategy : IEnemyAttackStrategy
{
    public ParticleSystem burnParticlesPrefab;
    public float burnDuration = 3f;
    private float interval = 0.2f;

    public BurnAttackStrategy(ParticleSystem burnParticlesPrefab, float burnDuration = 3f)
    {
        this.burnParticlesPrefab = burnParticlesPrefab;
        this.burnDuration = burnDuration;
    }

    public void Attack(EnemyBase enemy)
    {
        if (enemy.CurrentTowerTarget != null)
        {
            Debug.Log($"{enemy.name} đốt tower {enemy.CurrentTowerTarget.name}");

            // Kiểm tra xem đã có hiệu ứng cháy đang chạy chưa để tránh burn lại
            bool isBurning = false;
            ParticleSystem[] allParticles = enemy.CurrentTowerTarget.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var ps in allParticles)
            {
                if (ps.gameObject.name == burnParticlesPrefab.gameObject.name && ps.isPlaying)
                {
                    isBurning = true;
                    break;
                }
            }

            if (!isBurning && burnParticlesPrefab != null)
            {
                // Spawn hiệu ứng cháy làm con của tower
                ParticleSystem effect = GameObject.Instantiate(
                    burnParticlesPrefab,
                    enemy.CurrentTowerTarget.position + new Vector3(0,-0.9f,0), 
                    Quaternion.identity,
                    enemy.CurrentTowerTarget
                );
                effect.Play();

                // Bắt đầu gây damage over time khi hiệu ứng được tạo mới
                TowerBase tower = enemy.CurrentTowerTarget.GetComponent<TowerBase>();
                if (tower != null)
                {
                    MonoBehaviour runner = enemy.GetComponent<MonoBehaviour>();
                    if (runner == null) runner = tower.GetComponent<MonoBehaviour>();
                    if (runner != null)
                    {
                        runner.StartCoroutine(ApplyBurn(tower, effect,enemy));
                    }
                    else
                    {
                        Debug.LogWarning("Không tìm thấy MonoBehaviour để chạy Coroutine burn!");
                    }
                }
                else
                {
                    Debug.LogWarning("Tower không có component TowerBase để nhận damage!");
                }
            }
        }
    }

    // Coroutine gây damage và xóa hiệu ứng cháy khi xong
    private IEnumerator ApplyBurn(TowerBase tower, ParticleSystem burnEffect , EnemyBase enemyBase)
    {
        float elapsed = 0f;
        while (elapsed < burnDuration && tower != null)
        {
            float damageThisTick = enemyBase.attackDamage * interval;
            tower.TakeDamage(damageThisTick);
            elapsed += interval;
            yield return new WaitForSeconds(interval);
        }

        // Xóa hiệu ứng cháy nếu vẫn còn
        if (burnEffect != null)
        {
            burnEffect.Stop();
            // Chờ hiệu ứng tắt hoàn toàn rồi xóa đối tượng
            yield return new WaitForSeconds(burnEffect.main.startLifetime.constantMax);
            if (burnEffect != null)
                Object.Destroy(burnEffect.gameObject);
        }
    }
}