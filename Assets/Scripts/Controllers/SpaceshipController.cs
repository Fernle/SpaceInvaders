using System.Collections;
using Enums;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5f;
    public float reloadTimer = 1f;
    private float horizontalInput;
    
    public int maxHealth = 75;
    private int currentHealth;
    [SerializeField] private GameObject heart, heart2, heart3;
    public GameObject gameOverPanel;
    
    public GameObject bulletPrefab;
    public Transform[] bulletSpawnPoints;
    public Transform bulletSpawnPoint;
    private bool canShoot;
    [SerializeField] private AudioSource shootSound, collectingPerkSound;

    [SerializeField] private GameObject shield;
    private bool isShieldActive;
    private bool isMultiShootActive;
    private bool isBulletSpeedActive;
    private bool isDoubleScoreActive;

    void Start()
    {
        currentHealth = maxHealth;
        canShoot = true;
    }

    void Update()
    {
        SpaceshipMovement();
        HearthUI();

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
            heart.SetActive(false);
            gameOverPanel.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }

    private void HearthUI()
    {
        if (currentHealth == 75)
        {
            heart.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (currentHealth == 50)
        {
            heart.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if (currentHealth == 25)
        {
            heart.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
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
                    collectingPerkSound.Play();
                    isShieldActive = true;
                    StartCoroutine(ShieldCooldown());
                }
                break;
            
            case PerkType.MultipleShoots:
                if (!isMultiShootActive)
                {
                    collectingPerkSound.Play();
                    isMultiShootActive = true;
                    StartCoroutine(MultiShootCooldown());
                }
                break;
            
            case PerkType.BulletSpeed:
                if (!isBulletSpeedActive)
                {
                    collectingPerkSound.Play();
                    reloadTimer = 0.5f;
                    isBulletSpeedActive = true;
                    StartCoroutine(BulletSpedCooldown());
                }
                break;
            case PerkType.DoubleScore:
                if (!isDoubleScoreActive)
                {
                    collectingPerkSound.Play();
                    DoubleScoreController.Instance.ToggleMode();
                    isDoubleScoreActive = true;
                    StartCoroutine(DoubleScoreCooldown());
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

    IEnumerator DoubleScoreCooldown()
    {
        yield return new WaitForSeconds(10f);
        DoubleScoreController.Instance.ToggleMode();
        isDoubleScoreActive = false;
    }
}