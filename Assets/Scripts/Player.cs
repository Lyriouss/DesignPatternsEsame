using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static float playerMaxHealth;
    private float currentHealth;
    public static float maxShield;
    private float currentShield;
    private bool shieldActive = false;
    public static float damage;
    public static float damageRange;
    private bool damageActive = false;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject shieldObj;
    [SerializeField] private GameObject damageObj;

    public static event Action onShieldActivated, onShieldDeactivated;
    public static event Action<float> onPlayerHealed;
    public static event Action<float> onHealthDamageTaken, onShieldDamageTaken;

    private void OnEnable()
    {
        //shield ability events
        ShieldAbility.onShieldAbilityTriggered += ActivateShield;
        ShieldAbility.onShieldAbilityRemoved += DeactivateShield;
        //health ability event
        HealthAbility.onHealthAbilityTriggered += HealPlayer;
        //damage ability events
        DamageAbility.onDamageAbilityTriggered += ActivateDamage;
        DamageAbility.onDamageAbilityRemoved += DeactivateDamage;
    }

    private void OnDisable()
    {
        ShieldAbility.onShieldAbilityTriggered -= ActivateShield;
        ShieldAbility.onShieldAbilityRemoved -= DeactivateShield;
        
        HealthAbility.onHealthAbilityTriggered -= HealPlayer;
        
        DamageAbility.onDamageAbilityTriggered -= ActivateDamage;
        DamageAbility.onDamageAbilityRemoved -= DeactivateDamage;
    }

    private void Start()
    {
        //sets player health
        currentHealth = playerMaxHealth;
        //deactivates shield and damage objects
        shieldObj.SetActive(false);
        damageObj.SetActive(false);
    }

    private void Update()
    {
        //skips function if damage ability is not active
        if (!damageActive)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange, enemyMask);
        
        if (colliders.Length == 0)
            return;

        foreach (Collider enemy in colliders)
        {
            enemy.TryGetComponent(out IDamageable eDamageable);

            float trueDamage = damage * Time.deltaTime;

            eDamageable.TakeDamage(trueDamage);
        }
    }

    #region Shield
    private void ActivateShield()
    {
        //sets shield health
        currentShield = maxShield;
        //converts health damage to shield damage
        shieldActive = true;
        //activates shield bubble around player
        shieldObj.SetActive(true);
        //activates bar in ui
        onShieldActivated?.Invoke();
    }

    private void DeactivateShield()
    {
        //removes health to shield conversion
        shieldActive = false;
        //deactivates shield bubble
        shieldObj.SetActive(false);
        //deactivates bar in ui
        onShieldDeactivated?.Invoke();
    }
    #endregion
    
    #region Health
    private void HealPlayer()
    {
        //heals player to max health
        currentHealth = playerMaxHealth;
        //updates health bar in ui
        onPlayerHealed?.Invoke(1f);
    }
    #endregion
    
    #region Damage
    private void ActivateDamage()
    {
        //activates the overlap sphere in update
        damageActive = true;
        //activates damage area object
        damageObj.SetActive(true);
    }

    private void DeactivateDamage()
    {
        //disables the overlap sphere usage in update
        damageActive = false;
        //deactivates damage area object
        damageObj.SetActive(false);
    }
    #endregion

    public void TakeDamage(float damage)
    {
        //if shield is active, then takes damage on shield, if not then on health
        if (shieldActive)
        {
            //take damage on shield
            currentShield -= damage;

            //if shield is completely depleted
            if (currentShield <= 0f)
            {
                //deactivates shield
                shieldActive = false;
                onShieldDeactivated?.Invoke();
                //deactivates shield bubble
                shieldObj.SetActive(false);
            }
            else
            {
                //calculates fill amount for shield bar
                float shieldFill = currentShield / maxShield;
                
                //then sends float value to change bar fill amount in ui
                onShieldDamageTaken?.Invoke(shieldFill);
            }
        }
        else
        {
            //take damage on health
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                Despawn();
            }
            else
            {
                //calculates fill amount for health bar
                float healthFill = currentHealth / playerMaxHealth;
                
                //then sends float value to change bar fill amount in ui
                onHealthDamageTaken?.Invoke(healthFill);
            }
        }
    }

    public void Despawn()
    {
        Debug.Log("Despawn");
        //lose ability
        //teleport to spawn
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}
