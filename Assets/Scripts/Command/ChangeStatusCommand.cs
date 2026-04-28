using System;
using UnityEngine;

public class ChangeStatusCommand : ICommand
{
    private GameObject _pickup;
    private AbilityType _ability;
    
    public static event Action<bool, AbilityType> onAbilityChanged;

    public ChangeStatusCommand(GameObject pickup, AbilityType ability)
    {
        _pickup = pickup;
        _ability = ability;
        
        CommandManager.Instance.AddCommand(this);
    }
    
    public void Execute()
    {
        _pickup.SetActive(false);
        
        onAbilityChanged?.Invoke(true, _ability);
        
        // switch (_ability)
        // {
        //     case AbilityType.Shield:
        //         onShieldCollected?.Invoke();
        //         break;
        //     
        //     case AbilityType.Health:
        //         onHealthCollected?.Invoke();
        //         break;
        //     
        //     case AbilityType.Damage:
        //         onDamageCollected?.Invoke();
        //         break;
        // }
    }

    public void Undo()
    {
        _pickup.SetActive(true);
        
        onAbilityChanged?.Invoke(false, _ability);
        
        // switch (_ability)
        // {
        //     case AbilityType.Shield:
        //         onShieldRemoved?.Invoke();
        //         break;
        //     
        //     case AbilityType.Health:
        //         onHealthRemoved?.Invoke();
        //         break;
        //     
        //     case AbilityType.Damage:
        //         onDamageRemoved?.Invoke();
        //         break;
        // }
    }
}
