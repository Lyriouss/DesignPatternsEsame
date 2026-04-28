using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class AbilityUser : MonoBehaviour
{
    public static AbilityUser Instance;
    private IAbility ability;
    
    public static float speedDuration;
    public static float speedCooldown;
    public static float shieldDuration;
    public static float shieldCooldown;
    public static float healthCooldown;
    public static float damageDuration;
    public static float damageCooldown;
    private bool speedActive = false;
    private bool shieldActive = false;
    private bool healthActive = false;
    private bool damageActive = false;
    private bool shieldCollected = false;
    private bool healthCollected = false;
    private bool damageCollected = false;
    
    public static event Action<IAbility> onAbilityUsed;
    public static event Action<IAbility> onAbilityUsable;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }
    
    private void OnEnable()
    {
        //ability inputs that are Alpha1, Alpha2, Alpha3, and Alpha4 respectively
        InputManager.Instance.inputMap.Player.SpeedAbility.performed += SpeedInput;
        InputManager.Instance.inputMap.Player.ShieldAbility.performed += ShieldInput;
        InputManager.Instance.inputMap.Player.HealthAbility.performed += HealthInput;
        InputManager.Instance.inputMap.Player.DamageAbility.performed += DamageInput;

        ChangeStatusCommand.onAbilityChanged += ChangeAbilityStatus;
    }

    private void OnDisable()
    {
        InputManager.Instance.inputMap.Player.SpeedAbility.performed -= SpeedInput;
        InputManager.Instance.inputMap.Player.ShieldAbility.performed -= ShieldInput;
        InputManager.Instance.inputMap.Player.HealthAbility.performed -= HealthInput;
        InputManager.Instance.inputMap.Player.DamageAbility.performed -= DamageInput;
        
        ChangeStatusCommand.onAbilityChanged -= ChangeAbilityStatus;
    }

    private void ChangeAbilityStatus(bool status, AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.Shield:
                shieldCollected = status;
                break;
            
            case AbilityType.Health:
                healthCollected = status;
                break;
            
            case AbilityType.Damage:
                damageCollected = status;
                break;
        }
    }

    private void SpeedInput(InputAction.CallbackContext context)
    {
        UseSpeed();
    }

    public void UseSpeed()
    {
        //if ability is active, does not execute ability
        if (speedActive)
            return;
        
        //assign speed ability script to interface variable
        ability = new SpeedAbility();

        //sets bool to true so that ability doesn't run when active
        speedActive = true;
        
        //makes speed button non interactable
        onAbilityUsed?.Invoke(ability);

        //set ability on cooldown
        StartCoroutine(AbilityProcess(ability));
    }
    
    private void ShieldInput(InputAction.CallbackContext context)
    {
        UseShield();
    }

    public void UseShield()
    {
        //if ability is on cooldown or hasn't been collected yet, does not execute ability
        if (shieldActive || !shieldCollected)
            return;
        
        //assign shield ability script to interface variable
        ability = new ShieldAbility();

        //sets bool to true so that ability doesn't run when active
        shieldActive = true;
        
        //makes shield button non interactable
        onAbilityUsed?.Invoke(ability);

        //set ability on cooldown
        StartCoroutine(AbilityProcess(ability));
    }

    private void HealthInput(InputAction.CallbackContext context)
    {
        UseHealth();
    }

    public void UseHealth()
    {
        //if ability is on cooldown or hasn't been collected yet, does not execute ability
        if (healthActive || !healthCollected)
            return;
        
        //assign health ability script to interface variable
        ability = new HealthAbility();

        //sets bool to true so that ability doesn't run when active
        healthActive = true;
        
        //makes health button non interactable
        onAbilityUsed?.Invoke(ability);

        //set ability on cooldown
        StartCoroutine(AbilityProcess(ability));
    }

    private void DamageInput(InputAction.CallbackContext context)
    {
        UseDamage();
    }

    public void UseDamage()
    {
        //if ability is on cooldown or hasn't been collected yet, does not execute ability
        if (damageActive || !damageCollected)
            return;
        
        //assign damage ability script to interface variable
        ability = new DamageAbility();

        //sets bool to true so that ability doesn't run when active
        damageActive = true;
        
        //makes damage button non interactable
        onAbilityUsed?.Invoke(ability);

        //set ability on cooldown
        StartCoroutine(AbilityProcess(ability));
    }

    IEnumerator AbilityProcess(IAbility type)
    {
        //uses current ability
        type.UseAbility();
        
        switch (type)
        {
            case SpeedAbility:
                //waits for speed ability to end
                yield return new WaitForSeconds(speedDuration);
                //removes speed ability
                type.RemoveAbility();
                //waits for speed cooldown to end
                yield return new WaitForSeconds(speedCooldown);
                //allows speed ability to be used again
                speedActive = false;
                onAbilityUsable?.Invoke(type);
                break;
            
            case ShieldAbility:
                //waits for shield ability to end
                yield return new WaitForSeconds(shieldDuration);
                //removes shield ability
                type.RemoveAbility();
                //waits for shield cooldown to end
                yield return new WaitForSeconds(shieldCooldown);
                //allows shield ability to be used again
                shieldActive = false;
                onAbilityUsable?.Invoke(type);
                break;
            
            case HealthAbility:
                //waits for health cooldown to end
                yield return new WaitForSeconds(healthCooldown);
                //allows health ability to be used again
                healthActive = false;
                onAbilityUsable?.Invoke(type);
                break;
            
            case DamageAbility:
                //waits for damage ability to end
                yield return new WaitForSeconds(damageDuration);
                //removes damage ability
                type.RemoveAbility();
                //waits for damage cooldown to end
                yield return new WaitForSeconds(damageCooldown);
                //allows damage ability to be used again
                damageActive = false;
                onAbilityUsable?.Invoke(type);
                break;
        }
    }
}
