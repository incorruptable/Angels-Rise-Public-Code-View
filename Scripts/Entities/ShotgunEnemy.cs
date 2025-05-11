using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    [SerializeField]
    int health = 1;
    [SerializeField]
    GameObject deathVFX;
    [SerializeField]
    float explosionDuration = 1f;
    [SerializeField]
    AudioClip deathSFX;
    [SerializeField]
    [Range(0, 1)] float deathVolume = 0.7f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (other.tag == "Player Projectile")
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damage)
    {
        health -= damage.getDamage();
        if (health <= 0)
        {
            Destroy();
            Destroy(damage.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
    }
}
