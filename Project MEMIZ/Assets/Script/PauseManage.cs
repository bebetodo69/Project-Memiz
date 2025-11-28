using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public string menuSceneName = "Menu";  // nome da cena de menu (se usar botão Menu)

    private bool isPaused = false;

    void Update()
    {
        // Tecla para abrir/fechar o pause (Esc)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);   // mostra painel
        Time.timeScale = 0f;         // pausa o tempo do jogo
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);  // esconde painel
        Time.timeScale = 1f;          // volta o tempo ao normal
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}