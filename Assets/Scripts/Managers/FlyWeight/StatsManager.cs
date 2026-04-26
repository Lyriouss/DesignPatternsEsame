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
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerDamage;
    [SerializeField] private float playerRange;

    [Header("Ability Stats")] 
    [SerializeField] private float speedDuration;
    [SerializeField] private float speedCooldown;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float shieldCooldown;
    [SerializeField] private float healthCooldown;
    [SerializeField] private float damageDuration;
    [SerializeField] private float damageCooldown;

    //uses FlyWeight pattern to assign stats of enemy, player and bullet objects from one point
    private void Awake()
    {
        //enemy stats
        Enemy.enemyMaxHealth = enemyHealth;
        Enemy.range = enemyRange;           //used in Bullet + Enemy
        Enemy.fireRate = enemyFireRate;
        
        //bullet stats
        Bullet.damage = bulletDamage;
        Bullet.speed = bulletSpeed;
        Bullet.range = enemyRange;          //used in Bullet + Enemy
        
        //player stats
        PlayerMovement.speed = playerSpeed;
        Player.playerMaxHealth = playerHealth;
        Player.maxShield = playerShield;
        Player.damage = playerDamage;
        Player.damageRange = playerRange;
        
        //ability stats
        AbilityUser.speedDuration = speedDuration;
        AbilityUser.speedCooldown = speedCooldown;
        AbilityUser.shieldDuration = shieldDuration;
        AbilityUser.shieldCooldown = shieldCooldown;
        AbilityUser.healthCooldown = healthCooldown;
        AbilityUser.damageDuration = damageDuration;
        AbilityUser.damageCooldown = damageCooldown;
    }
}
