using System.Collections;
using UnityEngine;

public class SlowEffect : IBulletEffect {
    private IBulletEffect next;

    public SlowEffect(IBulletEffect next = null) {
        this.next = next;
    }

    public void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplySlow(enemy));
        next?.Apply(enemy);
    }

    private IEnumerator ApplySlow(EnemyBase enemy) {
        enemy.speed *= 0.5f;
        Debug.Log($"{enemy.name} bị slow");
        yield return new WaitForSeconds(2f);
        if (enemy != null) enemy.speed *= 2f;
        Debug.Log($"{enemy.name} hết slow");
    }
}
