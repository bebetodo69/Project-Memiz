using UnityEngine;
using UnityEngine.Events;
using static CommandManager;

public class ButtonCommandInvoker : MonoBehaviour
{
    private ICommand command;

    public void SetCommand(ICommand newCommand)
    {
        command = newCommand;
    }

    public void OnButtonPressed()
    {
        command?.Execute();
    }
}
