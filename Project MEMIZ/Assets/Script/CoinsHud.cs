using UnityEngine;
using TMPro;

public class CoinsHud : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    void OnEnable()
    {
        HudEvents.OnCoinsChanged += UpdateCoins;
    }

    void OnDisable()
    {
        HudEvents.OnCoinsChanged -= UpdateCoins;
    }

    void UpdateCoins(int value)
    {
        if (coinsText != null)
            coinsText.text = "Pontos: " + value;
    }
}