using System;

public class DamageAbility : IAbility
{
    public static event Action onDamageAbilityTriggered, onDamageAbilityRemoved;
    
    public void UseAbility()
    {
        //triggers event function in Player class that activates ability
        onDamageAbilityTriggered?.Invoke();
    }

    public void RemoveAbility()
    {
        //triggers event function in Player class that deactivates ability
        onDamageAbilityRemoved?.Invoke();
    }
}
