using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AbilityType 
{
    Speed,
    Shield,
    Health,
    Damage
}

public class AbilityUser : MonoBehaviour
{
    private IAbility ability;
    private AbilityType abilityType;
    
    [SerializeField] private float speedCooldown;
    [SerializeField] private float shieldCooldown;
    [SerializeField] private float healthCooldown;
    [SerializeField] private float damageCooldown;
    private bool speedOnCooldown = false;
    private bool shieldOnCooldown = false;
    private bool healthOnCooldown = false;
    private bool damageOnCooldown = false;

    private void OnEnable()
    {
        InputManager.Instance.inputMap.Player.SpeedAbility.performed += TriggerSpeed;
        InputManager.Instance.inputMap.Player.ShieldAbility.performed += TriggerShield;
        InputManager.Instance.inputMap.Player.HealthAbility.performed += TriggerHealth;
        InputManager.Instance.inputMap.Player.DamageAbility.performed += TriggerDamage;
    }

    private void OnDisable()
    {
        InputManager.Instance.inputMap.Player.SpeedAbility.performed -= TriggerSpeed;
        InputManager.Instance.inputMap.Player.ShieldAbility.performed -= TriggerShield;
        InputManager.Instance.inputMap.Player.HealthAbility.performed -= TriggerHealth;
        InputManager.Instance.inputMap.Player.DamageAbility.performed -= TriggerDamage;
    }

    private void TriggerSpeed(InputAction.CallbackContext context)
    {
        //if ability is on cooldown, does not execute ability
        if (speedOnCooldown)
            return;
        
        //assign speed ability script to interface variable
        ability = new SpeedAbility();
        //executes speed ability
        ability.UseAbility();
        //unassign script from interface
        ability = null;

        //sets cooldown bool to true to not execute function when on cooldown
        speedOnCooldown = true;
        //sets enum to ability type so coroutine knows which ability to turn off cooldown
        abilityType = AbilityType.Speed;

        //set ability on cooldown
        AbilityCooldown(speedCooldown, abilityType);
    }
    
    private void TriggerShield(InputAction.CallbackContext context)
    {
        //if ability is on cooldown, does not execute ability
        if (shieldOnCooldown)
            return;
        
        //assign shield ability script to interface variable
        ability = new ShieldAbility();
        //executes shield ability
        ability.UseAbility();
        //unassign script from interface
        ability = null;

        //sets cooldown bool to true to not execute function when on cooldown
        shieldOnCooldown = true;
        //sets enum to ability type so coroutine knows which ability to turn off cooldown
        abilityType = AbilityType.Shield;

        //set ability on cooldown
        AbilityCooldown(shieldCooldown, abilityType);
    }

    private void TriggerHealth(InputAction.CallbackContext context)
    {
        //if ability is on cooldown, does not execute ability
        if (healthOnCooldown)
            return;
        
        //assign health ability script to interface variable
        ability = new HealthAbility();
        //executes health ability
        ability.UseAbility();
        //unassign script from interface
        ability = null;

        //sets cooldown bool to true to not execute function when on cooldown
        healthOnCooldown = true;
        //sets enum to ability type so coroutine knows which ability to turn off cooldown
        abilityType = AbilityType.Health;

        //set ability on cooldown
        AbilityCooldown(healthCooldown, abilityType);
    }

    private void TriggerDamage(InputAction.CallbackContext context)
    {
        //if ability is on cooldown, does not execute ability
        if (damageOnCooldown)
            return;
        
        //assign damage ability script to interface variable
        ability = new DamageAbility();
        //executes damage ability
        ability.UseAbility();
        //unassign script from interface
        ability = null;

        //sets cooldown bool to true to not execute function when on cooldown
        damageOnCooldown = true;
        //sets enum to ability type so coroutine knows which ability to turn off cooldown
        abilityType = AbilityType.Damage;

        //set ability on cooldown
        AbilityCooldown(damageCooldown, abilityType);
    }

    IEnumerator AbilityCooldown(float cooldown, AbilityType type)
    {
        yield return new WaitForSeconds(cooldown);

        switch (type)
        {
            case AbilityType.Speed:
                speedOnCooldown = false;
                break;
            
            case AbilityType.Shield:
                shieldOnCooldown = false;
                break;
            
            case AbilityType.Health:
                healthOnCooldown = false;
                break;
            
            case AbilityType.Damage:
                damageOnCooldown = false;
                break;
        }
    }
}
