using UnityEngine;
using TMPro;

public class GameManagerPoints : MonoBehaviour
{
    public static GameManagerPoints Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
        HudEvents.OnCoinsChanged?.Invoke(score); // notifica valor inicial
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScoreText();
        HudEvents.OnCoinsChanged?.Invoke(score); // notifica HUD
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Pontos: " + score;
    }
}