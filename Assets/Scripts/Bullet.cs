using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    
    public static float damage;
    public static float speed;
    public static float range;

    private Vector3 spawnPoint;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {    
        //saves bullet spawn point as reference for distance traveled
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        //moves bullet forward at a constant speed
        Vector3 bulletMovement = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + bulletMovement);

        CheckDistance();
    }

    private void CheckDistance()
    {
        //calculates distance from spawn point to current point
        float distance = Vector3.Distance(spawnPoint, transform.position);

        //if the distance is greater than the range
        if (distance >= range)
        {
            //deactivate bullet
            gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //deactivates bullet colliding with player
            gameObject.SetActive(false);
        }
    }
}
