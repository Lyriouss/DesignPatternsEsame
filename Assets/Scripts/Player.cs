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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        //sets shield health and activates it
        currentShield = maxShield;
        shieldActive = true;
        
        //activates bar in ui
        onShieldActivated?.Invoke();

        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {
        //wait for the amount of time in shieldDuration until deactivating the shield
        yield return new WaitForSeconds(shieldDuration);
        shieldActive = false;
        
        //shield bar in ui is deactivated
        onShieldDeactivated?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (shieldActive)
        {
            currentShield -= damage;

            if (currentShield <= 0f)
            {
                shieldActive = false;
                onShieldDeactivated?.Invoke();
            }
            else
            {
                onShieldDamageTaken?.Invoke(currentShield);
            }
        }
        else
        {
            currentHealth -= damage;

            if (currentHealth <= 0f)
            {
                //lose ability
                //teleport to spawn
            }
            else
            {
                onHealthDamageTaken?.Invoke(currentHealth);
            }
        }
    }

    public void Despawn()
    {
        //teleport to spawn
    }
}
