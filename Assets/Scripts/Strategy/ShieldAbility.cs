using System;

public class ShieldAbility : IAbility
{
    public static event Action onShieldAbilityTriggered;
    
    public void UseAbility()
    {
        onShieldAbilityTriggered?.Invoke();
    }
}
