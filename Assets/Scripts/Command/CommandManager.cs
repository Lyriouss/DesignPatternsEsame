using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public static CommandManager Instance;

    //creates Stack to store order of actions for undo event
    private Stack<ICommand> undoStack = new();

    private void Awake()
    {
        //creates singleton of class
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    
    public void AddCommand(ICommand command)
    {
        //adds command interface to undo Stack
        undoStack.Push(command);

        //runs interface functions execute function
        command.Execute();
    }

    public void UndoCommand()
    {
        //only runs this function if there is at least one element present in undo Stack
        if (undoStack.Count <= 0)
            return;

        //removes last element from Stack and sets command state to last one recorded
        undoStack.Pop().Undo();
    }
}
