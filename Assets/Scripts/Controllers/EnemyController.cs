using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float shootingDelay;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float minX;
    public float maxX;
    public float movementDistance = 1f;

    private int direction = 1;
    private float initialX;

    private float currentShootingDelay;
    
    private int currentHealth = 25;
    
    public GameObject[] perkPrefabs;
    public float[] dropChances;

    void Start()
    {
        currentShootingDelay = shootingDelay;
        initialX = transform.position.x;
    }

    void Update()
    {
        EnemyMovement();
        EnemyShoot();
    }

    private void EnemyMovement()
    {
        float movement = speed * direction * Time.deltaTime;
        float newX = transform.position.x + movement;

        if (Mathf.Abs(newX - initialX) >= movementDistance)
        {
            direction *= -1;
        }

        float clampedX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void EnemyShoot()
    {
        currentShootingDelay -= Time.deltaTime;
        if (currentShootingDelay <= 0f)
        {
            Shoot();
            currentShootingDelay = shootingDelay;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            EnemyDied();
            Destroy(gameObject);
        }
    }

    private void EnemyDied()
    {
        for (int i = 0; i < perkPrefabs.Length; i++)
        {
            if (Random.value <= dropChances[i])
            {
                Instantiate(perkPrefabs[i], transform.position, Quaternion.identity);
                break; 
            }
        }
    }
}