using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PooledBullets pooledBullets;
    [SerializeField] private LayerMask playerMask;
    
    public static float health;
    public static float range;
    public static float fireRate;
    
    private float timer;

    private Transform currentTarget;

    private void Update()
    {
        //timer for fire rate 
        timer += Time.deltaTime;
        
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        //creates an overlap sphere around enemy to detect player
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, playerMask);

        //skips rest of function if no colliders are present in overlap sphere
        if (colliders.Length == 0)
            return;
        
        //sets closest player to max float
        float closestPlayer = float.MaxValue;

        foreach (Collider player in colliders)
        {
            //calculate the distance from player
            float distance = Vector3.Distance(player.transform.position, transform.position);

            //if the distance is less than the current closest player
            if (distance < closestPlayer)
            {
                //sets closest player to distance of this collider
                closestPlayer = distance;
                //player closest to enemy becomes target
                currentTarget = player.transform;
            }
        }

        //looks towards the target player
        transform.LookAt(currentTarget);
        
        //shoots bullet after getting target
        BulletShooter();
    }

    private void BulletShooter()
    {
        //shoots a bullet the equivalent of time in fireRate
        if (timer >= fireRate)
        {
            //uses object pooler to spawn bullets
            pooledBullets.GetBullet(transform.position, transform.rotation);
            //sets timer back to zero
            timer = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
