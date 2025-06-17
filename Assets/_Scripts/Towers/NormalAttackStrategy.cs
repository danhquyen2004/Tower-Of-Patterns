using UnityEngine;

public class NormalAttackStrategy : IAttackStrategy
{
    private GameObject bulletObj;
    BulletBase bullet;
    public NormalAttackStrategy() {
        bulletObj = Resources.Load<GameObject>($"Bullets/Bullet_Normal");
    }
    public void Execute(TowerBase tower, EnemyBase target) {
        GameObject bulletObject = GameObject.Instantiate(bulletObj, tower.transform.position, Quaternion.identity);
        bullet = bulletObject.GetComponent<BulletBase>();
        bullet.Initialize(10f, 4f, null, target); // Initialize bullet with damage and speed
        Debug.Log($"{tower.name} bắn {target.name} gây 10 damage");
    }
}
