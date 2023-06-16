using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (!IsInCameraView())
        {
            Destroy(gameObject);
        }
    }

    bool IsInCameraView()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        return screenPoint.y > 0 && screenPoint.y < 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpaceshipController spaceshipHealth = collision.GetComponent<SpaceshipController>();
            if (spaceshipHealth != null)
            {
                spaceshipHealth.TakeDamage(25);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision, true);
        }
    }
}