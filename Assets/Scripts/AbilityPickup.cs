using System;
using UnityEngine;

public enum AbilityType
{
    Shield,
    Health,
    Damage
}

public class AbilityPickup : MonoBehaviour, ICollectible
{
    [SerializeField] private AbilityType abilityType;
    
    public static event Action onShieldCollected, onHealthCollected, onDamageCollected;
    
    public void Collect()
    {
        switch (abilityType)
        {
            case AbilityType.Shield:
                onShieldCollected?.Invoke();
                break;
            
            case AbilityType.Health:
                onHealthCollected?.Invoke();
                break;
            
            case AbilityType.Damage:
                onDamageCollected?.Invoke();
                break;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
