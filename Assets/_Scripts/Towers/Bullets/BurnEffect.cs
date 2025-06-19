using System.Collections;
using UnityEngine;

public class BurnEffect : BulletEffectDecorator {
    public float duration = 3f;
    private float interval = 1f;
    private float damage = 5f;
    private float burnRadius = 1.1f;

    public BurnEffect(IBulletEffect next = null) : base(next) { }

    public override void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplyBurn(enemy));
        base.Apply(enemy);
    }

    private IEnumerator ApplyBurn(EnemyBase enemyHitted) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            Collider2D[] hits = Physics2D.OverlapCircleAll(enemyHitted.transform.position, burnRadius);
            foreach (var hit in hits) {
                EnemyBase enemy = hit.GetComponent<EnemyBase>();
                if (enemy != null) {
                    enemy.TakeDamage(damage);
                    Debug.Log($"{enemy.name} bị đốt, mất {damage} HP");
                }
            }

            elapsedTime += interval;
            yield return new WaitForSeconds(interval);
        }
    }
}