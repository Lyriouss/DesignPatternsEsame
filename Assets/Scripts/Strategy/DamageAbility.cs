using System;

public class DamageAbility : IAbility
{
    public static event Action onDamageAbilityTriggered;
    
    public void UseAbility()
    {
        onDamageAbilityTriggered?.Invoke();
    }
}
