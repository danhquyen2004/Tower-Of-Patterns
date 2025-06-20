using UnityEngine;
using System;

public class GoldManager : MonoBehaviour {
    public static GoldManager Instance { get; private set; }

    public int Gold { get; private set; } = 200;
    public event Action<int> OnGoldChanged;

    private void Awake() {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        ResetGold();
    }

    public void AddGold(int amount) {
        Gold += amount;
        OnGoldChanged?.Invoke(Gold);
        Debug.Log($"Gold added: {amount}, current gold: {Gold}");
    }

    public bool SpendGold(int amount) {
        if (Gold >= amount) {
            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            Debug.Log($"Gold spent: {amount}, current gold: {Gold}");
            return true;
        }
        Debug.Log("Not enough gold!");
        return false;
    }
    
    public void ResetGold() {
        Gold = 200;
        OnGoldChanged?.Invoke(Gold);
    }
}
