using System.Collections;
using UnityEngine;

public class SlowEffect : BulletEffectDecorator {
    public float slowAmount = 0.5f; // Giảm tốc độ xuống 50%
    public float slowDuration = 2f; // Thời gian hiệu lực của hiệu ứng slow

    public SlowEffect(IBulletEffect next = null) : base(next) { }

    public override void Apply(EnemyBase enemy) {
        enemy.StartCoroutine(ApplySlow(enemy));
        base.Apply(enemy);
    }

    private IEnumerator ApplySlow(EnemyBase enemy) {
        enemy.speed *= slowAmount;
        Debug.Log($"{enemy.name} bị slow");
        yield return new WaitForSeconds(slowDuration);
        if (enemy != null) enemy.speed *= 2f;
        Debug.Log($"{enemy.name} hết slow");
    }
}
