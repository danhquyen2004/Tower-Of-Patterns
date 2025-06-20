using System;
using UnityEngine;
using CodeMonkey.HealthSystemCM;


public class CastleController : MonoBehaviour, IGetHealthSystem
{
    public float maxHealth = 100f;
    private HealthSystem healthSystem;
    
    private void Awake()
    {
        healthSystem = new HealthSystem(maxHealth);
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    public void Restart()
    {
        healthSystem.Heal(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        healthSystem.Damage(damage);
        Debug.Log($"{name} nhận {damage} sát thương, máu hiện tại: {healthSystem.GetHealth()}");
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        GameManager.Instance.GameOver();
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
