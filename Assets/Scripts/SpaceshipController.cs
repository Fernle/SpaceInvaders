using System.Collections;
using Enums;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;
    public float reloadTimer = 1f;
    private float horizontalInput;
    
    public int maxHealth = 100;
    private int currentHealth;
    
    public GameObject bulletPrefab;
    public Transform[] bulletSpawnPoints;
    public Transform bulletSpawnPoint;
    private bool canShoot;
    [SerializeField] private AudioSource shootSound;

    [SerializeField] private GameObject shield;
    private bool isShieldActive;
    private bool isMultiShootActive;
    private bool isBulletSpeedActive;
    
    void Start()
    {
        currentHealth = maxHealth;
        canShoot = true;
    }

    void Update()
    {
        SpaceshipMovement();

        if (Input.GetKeyDown(KeyCode.Space) && canShoot) SpaceshipShoot();
    }

    private void SpaceshipMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }

    private void SpaceshipShoot()
    {
        if (isMultiShootActive)
        {
            foreach (Transform shootPoint in bulletSpawnPoints)
            {
                Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }
        shootSound.Play();
        canShoot = false;
        StartCoroutine(ReloadTimer());
    }

    IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTimer);
        canShoot = true;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        { 
            Destroy(gameObject);
        }
    }

    public void ApplyPerk(PerkType perkType)
    {
        switch (perkType)
        {
            case PerkType.ShieldBarrier:
                if (!isShieldActive)
                {
                    shield.SetActive(true);
                    isShieldActive = true;
                    StartCoroutine(ShieldCooldown());
                }
                break;
            
            case PerkType.MultipleShoots:
                if (!isMultiShootActive)
                {
                    isMultiShootActive = true;
                    StartCoroutine(MultiShootCooldown());
                }
                break;
            
            case PerkType.BulletSpeed:
                if (!isBulletSpeedActive)
                {
                    reloadTimer = 0.5f;
                    isBulletSpeedActive = true;
                    StartCoroutine(BulletSpedCooldown());
                }
                break;
        }
    }

    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
        isShieldActive = false;
    }

    IEnumerator MultiShootCooldown()
    {
        yield return new WaitForSeconds(5f);
        isMultiShootActive = false;
    }

    IEnumerator BulletSpedCooldown()
    {
        yield return new WaitForSeconds(3f);
        reloadTimer = 1f;
        isBulletSpeedActive = false;
    }
}