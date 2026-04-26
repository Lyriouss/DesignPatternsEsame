using System;

public class HealthAbility : IAbility
{
    public static event Action onHealthAbilityTriggered;
    
    public void UseAbility()
    {
        onHealthAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        
    }
}
