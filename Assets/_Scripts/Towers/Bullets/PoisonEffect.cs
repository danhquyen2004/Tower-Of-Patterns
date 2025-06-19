using System.Collections;
using UnityEngine;

public class PoisonEffect : BulletEffectDecorator {
    public float duration = 5f;
    private float interval = 1f;

    public PoisonEffect(IBulletEffect next = null) : base(next) { }

    public override void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplyPoison(enemy));
        base.Apply(enemy);
    }

    private IEnumerator ApplyPoison(EnemyBase enemy) {
        int ticks = Mathf.CeilToInt(duration / interval);
        for (int i = 0; i < ticks; i++) {
            if (enemy == null) yield break;
            enemy.TakeDamage(3);
            Debug.Log($"{enemy.name} bị poison, mất 3 HP");
            yield return new WaitForSeconds(interval);
        }
    }
}