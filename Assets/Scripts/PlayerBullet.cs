using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        
        if (!IsInCameraView())
        {
            Destroy(gameObject);
        }
    }

    bool IsInCameraView()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPoint.y < 0 || screenPoint.y > 1)
        {
            return false;
        }

        return true;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController spaceshipHealth = collision.GetComponent<EnemyController>();
            if (spaceshipHealth != null)
            {
                spaceshipHealth.TakeDamage(25);
            }
            Destroy(gameObject);
        }
    }
}
