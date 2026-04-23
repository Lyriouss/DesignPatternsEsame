using System;
using UnityEngine;

public class SpeedAbility : IAbility
{
    public static event Action onSpeedAbilityTriggered;
    
    public void UseAbility()
    {
        onSpeedAbilityTriggered?.Invoke();
    }
}
