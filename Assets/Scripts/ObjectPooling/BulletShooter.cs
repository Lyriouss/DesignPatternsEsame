using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private PooledBullets pooledBullets;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            pooledBullets.GetBullet(transform.position);
            timer = 0f;
        }
    }
}
