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
    
    public void TakeDamage(float damage)
    {
        healthSystem.Damage(damage);
        Debug.Log($"{name} nhận {damage} sát thương, máu hiện tại: {healthSystem.GetHealth()}");
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Debug.Log($"{name} đã chết");
        // Xử lý khi tower chết, ví dụ: hủy đối tượng
        Destroy(gameObject);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
