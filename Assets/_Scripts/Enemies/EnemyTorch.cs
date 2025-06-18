public class EnemyTorch : EnemyBase {
    protected override void Awake() {
        base.Awake();
        attackStrategy = new BurnAttackStrategy();

    }
}

