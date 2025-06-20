using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private Button startWaveButton;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timeCountText;

    public void SetButtonEnable(bool isEnable)
    {
        startWaveButton.gameObject.SetActive(isEnable);
    }
    public void SetWaveText(int waveNumber)
    {
        waveText.text = $"Wave {waveNumber}";
    }

    public void SetTimeCountText(float timeCount)
    {
        timeCount = (int)timeCount;
        if (timeCount < 60)
        {
            if(timeCount < 10)
                timeCountText.text = "00:0" + timeCount;
            else
                timeCountText.text = "00:" + timeCount;
        }
        else
        {
            timeCountText.text = "01:00";
        }
        
    }
}
