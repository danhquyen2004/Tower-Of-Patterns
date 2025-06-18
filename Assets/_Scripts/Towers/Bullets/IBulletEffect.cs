public interface IBulletEffect {
    void Apply(EnemyBase enemy);
    IBulletEffect SetNext(IBulletEffect next);
}