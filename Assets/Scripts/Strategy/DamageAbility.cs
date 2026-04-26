using System;

public class DamageAbility : IAbility
{
    public static event Action onDamageAbilityTriggered, onDamageAbilityRemoved;
    
    public void UseAbility()
    {
        onDamageAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        onDamageAbilityRemoved?.Invoke();
    }
}
