using UnityEngine;

public enum AbilityType
{
    Shield,
    Health,
    Damage
}

public class AbilityPickup : MonoBehaviour, ICollectible
{
    //enum selected in inspector for ability to unlock when collecting
    [SerializeField] private AbilityType abilityType;
    
    private void OnEnable()
    {
        //sets position of ability pickup at a random point in arena whenever pickup object is reactivated
        transform.position = new Vector3(Random.Range(-25f, 25f), 1f, Random.Range(-25f, 25f));
    }
    
    public void Collect()
    {
        //passes ability pickup and enum type to save for undo Stack
        new ChangeStatusCommand(gameObject, abilityType);
    }

    private void OnTriggerEnter(Collider other)
    {
        //only collects pickup if Player enters pickup
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }
}
