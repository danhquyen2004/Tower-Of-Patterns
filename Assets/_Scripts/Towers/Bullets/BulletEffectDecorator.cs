using UnityEngine;

public abstract class BulletEffectDecorator : IBulletEffect {
    protected IBulletEffect next;

    public BulletEffectDecorator(IBulletEffect next = null) {
        this.next = next;
    }

    public virtual void Apply(EnemyBase enemy) {
        next?.Apply(enemy);
    }

    public IBulletEffect SetNext(IBulletEffect next) {
        this.next = next;
        return this;
    }
}