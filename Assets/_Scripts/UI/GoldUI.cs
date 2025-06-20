using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI goldText;
    

    private void Update()
    {
        UpdateGoldDisplay();   
    }

    private void UpdateGoldDisplay()
    {
        goldText.text = GoldManager.Instance.Gold.ToString();
    }
}