using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static float maxHealth;
    private float currentHealth;
    public static float maxShield;
    private float currentShield;
    public static float shieldDuration;
    private bool shieldActive;
    public static float damage;
    public static float damageRange;

    public static event Action onShieldActivated, onShieldDeactivated;
    public static event Action<float> onHealthDamageTaken, onShieldDamageTaken;

    private void Start()
    {
        //sets player health
        currentHealth = maxHealth;
    }
    
    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateShield();
        }
    }
    
    #region Speed
    private void ChangeSpeed()
    {
        
    }
    
    #endregion

    #region Shield
    private void ActivateShield()
    {
        //sets shield health and activates it
        currentShield = maxShield;
        shieldActive = true;
        
        //activates bar in ui
        onShieldActivated?.Invoke();

        StartCoroutine(Shield(shieldDuration));
    }

    IEnumerator Shield(float duration)
    {
        //wait for the amount of time in duration until deactivating the shield
        yield return new WaitForSeconds(duration);
        
        //deactivates shield
        shieldActive = false;
        onShieldDeactivated?.Invoke();
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
                float healthFill = currentHealth / maxHealth;
                
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
}
