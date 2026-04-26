using System;

public class ShieldAbility : IAbility
{
    public static event Action onShieldAbilityTriggered, onShieldAbilityRemoved;
    
    public void UseAbility()
    {
        onShieldAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        onShieldAbilityRemoved?.Invoke();
    }
}
