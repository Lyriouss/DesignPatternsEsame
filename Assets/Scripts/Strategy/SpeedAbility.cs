using System;
using UnityEngine;

public class SpeedAbility : IAbility
{
    public static event Action onSpeedAbilityTriggered, onSpeedAbilityRemoved;
    
    public void UseAbility()
    {
        onSpeedAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        onSpeedAbilityRemoved?.Invoke();
    }
}
