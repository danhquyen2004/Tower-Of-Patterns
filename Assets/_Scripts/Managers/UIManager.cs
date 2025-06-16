using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void UpdateGold(int gold) {
        Debug.Log($"UI update gold: {gold}");
        // Cập nhật text UI thật sự ở đây
    }

    public void UpdateBaseHealth(int health) {
        Debug.Log($"UI update base health: {health}");
        // Update UI
    }

    public void UpdateWave(int wave) {
        Debug.Log($"UI update wave: {wave}");
        // Update wave text
    }
}
