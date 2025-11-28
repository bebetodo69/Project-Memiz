using System.IO;
using UnityEngine;

public static class JsonSaveSystem
{
    private static string saveFileName = "savegame.json";

    private static string GetFullPath()
    {
        return Path.Combine(Application.persistentDataPath, saveFileName);
    }

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFullPath(), json);
        Debug.Log("Jogo salvo em: " + GetFullPath());
    }

    public static bool HasSave()
    {
        return File.Exists(GetFullPath());
    }

    public static SaveData LoadGame()
    {
        if (!HasSave())
        {
            Debug.LogWarning("Nenhum save encontrado.");
            return null;
        }

        string json = File.ReadAllText(GetFullPath());
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }
}