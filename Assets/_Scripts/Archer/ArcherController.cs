using UnityEngine;

public class ArcherController : MonoBehaviour
{
    private Animator animator;
    private TowerBase towerBase;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        towerBase = GetComponentInParent<TowerBase>();
    }

    public void Shoot()
    {
        if (animator != null)
        {
            animator.SetBool("isShoot", true);
        }
    }

    public void StopAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isShoot", false);
        }
    }

    // Gọi bởi Animation Event
    public void ReleaseArrow()
    {
        if (towerBase != null)
        {
            EnemyBase target = towerBase.FindTarget();
            if (target != null)
            {
                towerBase.attackStrategy?.Execute(towerBase, target);
            }
        }
    }

    // Cập nhật tốc độ animation dựa trên fireRate
    public void UpdateAnimationSpeed(float fireRate)
    {
        if (animator != null)
        {
            animator.speed = fireRate;
        }
    }
}