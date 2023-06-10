using UnityEngine;
using Enums;

public class Perk : MonoBehaviour
{
    public float moveSpeed = 3f;
    public PerkType perkType;

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpaceshipController spaceship = collision.GetComponent<SpaceshipController>();
            if (spaceship != null)
            {
                spaceship.ApplyPerk(perkType);
            }
            Destroy(gameObject);
        }
    }
}
