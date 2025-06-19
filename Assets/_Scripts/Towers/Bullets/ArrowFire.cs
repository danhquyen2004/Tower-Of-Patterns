using System;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowFire : ArrowBase
{
    [SerializeField] ParticleSystem particle;
    
    private void Awake()
    {
        effect = new BurnEffect();
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null) {
            Debug.Log("Bullet hit: " + enemy.name);
            enemy.TakeDamage(tower.damage);
            effect?.Apply(enemy);
            if (particle != null)
            {
                ParticleSystem ps = Instantiate(particle, other.transform.position, Quaternion.identity);
                ps.Play();
                Destroy(ps.gameObject, ((BurnEffect)effect).duration);
            }
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
