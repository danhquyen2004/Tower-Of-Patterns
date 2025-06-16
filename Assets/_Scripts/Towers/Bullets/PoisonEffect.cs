using System.Collections;
using UnityEngine;

public class PoisonEffect : IBulletEffect {
    private IBulletEffect next;

    public PoisonEffect(IBulletEffect next = null) {
        this.next = next;
    }

    public void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplyPoison(enemy));
        next?.Apply(enemy);
    }

    private IEnumerator ApplyPoison(EnemyBase enemy) {
        float duration = 5f;
        float interval = 1f;
        int ticks = Mathf.CeilToInt(duration / interval);
        for (int i = 0; i < ticks; i++) {
            if (enemy == null) yield break;
            enemy.TakeDamage(3);
            Debug.Log($"{enemy.name} bị poison, mất 3 HP");
            yield return new WaitForSeconds(interval);
        }
    }
}
