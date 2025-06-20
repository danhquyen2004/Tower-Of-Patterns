using System;
using Unity.VisualScripting;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI goldText;


    private void Start()
    {
        GoldManager.Instance.OnGoldChanged += UpdateGoldDisplay;
    }
    

    private void UpdateGoldDisplay(int newGoldAmount)
    {
        goldText.text = GoldManager.Instance.Gold.ToString();
    }
}