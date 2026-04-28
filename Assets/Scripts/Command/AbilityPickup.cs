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
        new ChangeStatusCommand(gameObject, abilityType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
