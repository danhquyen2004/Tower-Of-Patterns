public interface IEnemyState
{
    void Enter(EnemyBase enemy); // Gọi khi bắt đầu vào state này
    void Update(EnemyBase enemy); // Gọi mỗi frame khi ở state này
    void Exit(EnemyBase enemy); // Gọi khi rời khỏi state này
}