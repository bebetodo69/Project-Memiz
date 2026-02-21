using UnityEngine;
using UnityEngine.UI;
using static CommandManager;

public class PauseMenuSetup : MonoBehaviour
{
    public Button resumeButton;
    public Button restartButton;
    public Button menuButton;

    private void Start()
    {
        var resumeInvoker = resumeButton.GetComponent<ButtonCommandInvoker>();
        var restartInvoker = restartButton.GetComponent<ButtonCommandInvoker>();
        var menuInvoker   = menuButton.GetComponent<ButtonCommandInvoker>();

        resumeInvoker.SetCommand(new ResumeCommand());
        restartInvoker.SetCommand(new RestartLevelCommand());
        menuInvoker.SetCommand(new GoToMenuCommand());

        resumeButton.onClick.AddListener(resumeInvoker.OnButtonPressed);
        restartButton.onClick.AddListener(restartInvoker.OnButtonPressed);
        menuButton.onClick.AddListener(menuInvoker.OnButtonPressed);
    }
}
