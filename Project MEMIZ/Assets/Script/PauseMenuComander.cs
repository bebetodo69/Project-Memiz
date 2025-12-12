using UnityEngine;
using static CommandManager;

public class ResumeCommand : ICommand
{
    public void Execute()
    {
        PauseManager.Instance.ResumeGame();
    }
}

public class RestartLevelCommand : ICommand
{
    public void Execute()
    {
        PauseManager.Instance.RestartLevel();
    }
}

public class GoToMenuCommand : ICommand
{
    public void Execute()
    {
        PauseManager.Instance.GoToMenu();
    }
}
