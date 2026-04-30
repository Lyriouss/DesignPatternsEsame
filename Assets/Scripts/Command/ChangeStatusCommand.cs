using System;
using UnityEngine;

public class ChangeStatusCommand : ICommand
{
    private GameObject _pickup;
    private AbilityType _ability;
    
    //event action used in UiManager and AbilityUser to change status of abilities
    public static event Action<bool, AbilityType> onAbilityChanged;

    public ChangeStatusCommand(GameObject pickup, AbilityType ability)
    {
        //saves in this class the ability object and enum type
        _pickup = pickup;
        _ability = ability;
        
        //then passes this class to save in undo Stack
        CommandManager.Instance.AddCommand(this);
    }
    
    public void Execute()
    {
        //deactivates ability pickup collected
        _pickup.SetActive(false);
        
        //allows ability to be used in AbilityUser and updates UI button relative to ability type
        onAbilityChanged?.Invoke(true, _ability);
    }

    //does the exact opposite of Execute function
    public void Undo()
    {
        _pickup.SetActive(true);
        
        onAbilityChanged?.Invoke(false, _ability);
    }
}
