using UnityEngine;

public class NormalAttackStrategy : IAttackStrategy
{
    private GameObject bulletObj;
    public ArrowBase Arrow;
    public NormalAttackStrategy() {
        bulletObj = Resources.Load<GameObject>($"Bullets/Bullet_Normal");
    }
    public void Execute(TowerBase towerBase, EnemyBase target) {
        GameObject bulletObject = ObjectPooling.Instance.GetObject(bulletObj);
        //GameObject bulletObject = GameObject.Instantiate(bulletObj, towerBase.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        Arrow = bulletObject.GetComponent<ArrowBase>();
        Arrow.Initialize(towerBase, target); // Initialize bullet with damage and speed
        bulletObject.transform.position = towerBase.transform.position + new Vector3(0, 0.7f, 0);
        bulletObject.SetActive(true);
        Debug.Log($"Tower Normal bắn {target.name} gây 10 damage");
    }
}
