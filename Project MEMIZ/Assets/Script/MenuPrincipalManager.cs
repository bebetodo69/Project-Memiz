using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Fase1";

    public void NewGame()
    {
        GameSession.loadFromSave = false;  
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void ContinueGame()
    {
    //     string path = Path.Combine(Application.persistentDataPath, "savegame.json");
    //     if (File.Exists(path))
    //     {
    //         GameSession.loadFromSave = true;
    //         Time.timeScale = 1f;
    //         SceneManager.LoadScene(gameSceneName);
    //     }
    //     else
    //     {
    //         Debug.Log("Nenhum save encontrado, iniciando novo jogo.");
    //         NewGame();
    //     }
        SaveData data = JsonSaveSystem.LoadGame();
    }

    
}
