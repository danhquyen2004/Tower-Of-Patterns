using System.Collections;
using UnityEngine;

public class BurnEffect : IBulletEffect {
    private IBulletEffect next;

    public BurnEffect(IBulletEffect next = null) {
        this.next = next;
    }
    
    public void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplyBurn(enemy));
        next?.Apply(enemy);
    }

    private IEnumerator ApplyBurn(EnemyBase enemy) {
        float duration = 3f;
        float interval = 1f;
        int ticks = Mathf.CeilToInt(duration / interval);
        for (int i = 0; i < ticks; i++) {
            if (enemy == null) yield break;
            enemy.TakeDamage(5);
            Debug.Log($"{enemy.name} bị burn, mất 5 HP");
            yield return new WaitForSeconds(interval);
        }
    }
}
