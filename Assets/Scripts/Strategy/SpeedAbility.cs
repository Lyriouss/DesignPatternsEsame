using System;

public class SpeedAbility : IAbility
{
    public static event Action onSpeedAbilityTriggered, onSpeedAbilityRemoved;
    
    public void UseAbility()
    {
        //triggers event function in PlayerMovement class that activates ability
        onSpeedAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        //triggers event function in PlayerMovement class that deactivates ability
        onSpeedAbilityRemoved?.Invoke();
    }
}
