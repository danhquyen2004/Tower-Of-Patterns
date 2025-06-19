using UnityEngine;

public class ArrowIce : ArrowBase
{
    [SerializeField] ParticleSystem particle;
    
    private void Awake()
    {
        effect = new SlowEffect();
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
                ParticleSystem ps = Instantiate(particle, enemy.transform);
                ps.transform.localPosition = Vector3.zero; // Ensure it aligns with the enemy's position
                ps.Play();
                Destroy(ps.gameObject, ((SlowEffect)effect).slowDuration);
            }
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
