using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float enemyHealth;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float enemyRange;
    [SerializeField] private float enemyFireRate;

    [Header("Player Stats")] 
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerShield;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float speedBoostDuration;
    [SerializeField] private float playerDamage;
    [SerializeField] private float playerRange;

    //uses FlyWeight pattern to assign stats of enemy, player and bullet objects from one point
    private void Awake()
    {
        //enemy stats
        Enemy.health = enemyHealth;
        Enemy.range = enemyRange;
        Enemy.fireRate = enemyFireRate;
        
        //bullet stats
        Bullet.damage = bulletDamage;
        Bullet.speed = bulletSpeed;
        Bullet.range = enemyRange;
        
        //player stats
        PlayerMovement.speed = playerSpeed;
        PlayerMovement.speedBoostDuration = speedBoostDuration;
        Player.maxHealth = playerHealth;
        Player.maxShield = playerShield;
        Player.shieldDuration = shieldDuration;
        Player.damage = playerDamage;
        Player.damageRange = playerRange;
    }
}
