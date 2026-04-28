using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;

    private Stack<ICommand> undoStack = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    
    public void AddCommand(ICommand command)
    {
        undoStack.Push(command);

        command.Execute();
    }

    public void UndoCommand()
    {
        if (undoStack.Count <= 0)
            return;

        undoStack.Pop().Undo();
    }
}
