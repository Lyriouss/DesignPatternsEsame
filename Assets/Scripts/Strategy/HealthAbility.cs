using System;

public class HealthAbility : IAbility
{
    public static event Action onHealthAbilityTriggered;
    
    public void UseAbility()
    {
        //triggers event function in Player class that activates ability
        onHealthAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        //since the ability doesn't have a duration, remove ability isn't used
    }
}
