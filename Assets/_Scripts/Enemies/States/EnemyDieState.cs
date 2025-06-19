using UnityEngine;

    public class EnemyDieState : IEnemyState
    {
        public void Enter(EnemyBase enemy)
        {
            // Play death animation, disable collider, etc.
            enemy.enabled = false; // Stop further updates
            // Optionally: enemy.GetComponent<Collider2D>().enabled = false;
            // Optionally: enemy.GetComponent<SpriteRenderer>().color = Color.gray;
            enemy.Invoke(nameof(enemy.Die), 0.5f); // Delay destroy for effect
        }

        public void Update(EnemyBase enemy)
        {
            // No-op: Enemy is dead
        }

        public void Exit(EnemyBase enemy)
        {
            // Cleanup if needed
        }
    }