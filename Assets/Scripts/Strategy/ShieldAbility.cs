using System;

public class ShieldAbility : IAbility
{
    public static event Action onShieldAbilityTriggered, onShieldAbilityRemoved;
    
    public void UseAbility()
    {
        //triggers event function in Player class that activates ability
        onShieldAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        //triggers event function in Player class that deactivates ability
        onShieldAbilityRemoved?.Invoke();
    }
}
