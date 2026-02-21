using UnityEngine;
using UnityEngine.UI;

public class HeartSysten : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    public void AtualizarCoroes()
    {
        if (vida > vidaMaxima)
            vida = vidaMaxima;

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
                coracao[i].sprite = cheio;
            else
                coracao[i].sprite = vazio;

            coracao[i].enabled = i < vidaMaxima;
        }

        HudEvents.OnLifeChanged?.Invoke(vida); // notifica HUD de vida
    }
}