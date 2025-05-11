using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons
{
    public int damage { get; protected set; }
    public float projectileSpeed { get; protected set; }
    public float fireRate { get; protected set; }
    public GameObject projectilePrefab { get; protected set; }
    public abstract void Fire(Transform firePoint, AudioClip firingSFX);
    public abstract void Fire(Vector3 position, Quaternion rotation, AudioClip firingSFX);

    protected void PlaySFX (AudioClip clip, Vector3 position)
    {
        float volume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SFXVolume, 0.7f);
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}

public class RocketWeapon : Weapons
{
    private Vector3 targetPosition;
    public RocketWeapon(GameObject prefab, Vector3 targetPos, float projectileSpeed = 5f)
    {
        damage = 10;
        this.projectileSpeed = projectileSpeed;
        fireRate = 5f;
        projectilePrefab = prefab;
        this.targetPosition = targetPos;
    }

    public override void Fire(Transform firePoint, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();

        //Runs the homing coroutine to adjust the shot over time.
        projectile.AddComponent<HomingBehavior>().Initialize(targetPosition, projectileSpeed, rigidBody);
        PlaySFX(firingSFX, firePoint.position);
    }

    public override void Fire(Vector3 position, Quaternion rotation, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, position, rotation);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector3.up * projectileSpeed;
        Debug.Log($"Projectile speed set to: {projectile.GetComponent<Rigidbody2D>().linearVelocity}. Should be {position * projectileSpeed}");
        if (firingSFX != null)
            PlaySFX(firingSFX, position);
        else Debug.LogWarning("No firing sound effect assigned.");
    }
}

public class DefaultWeapon : Weapons
{
    public DefaultWeapon(GameObject prefab, Transform firePoint, float projectileSpeed = 30f)
    {
        damage = 2;
        this.projectileSpeed = projectileSpeed;
        fireRate = 1f;
        projectilePrefab = prefab;
    }

    public override void Fire(Transform firePoint, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = firePoint.up * projectileSpeed;
        Debug.Log($"Projectile speed set to: {projectile.GetComponent<Rigidbody2D>().linearVelocity}. Should be {firePoint.up * projectileSpeed}");
        if (firingSFX != null)
            PlaySFX(firingSFX, firePoint.position);
        else Debug.LogWarning("No firing sound effect assigned.");
    }

    public override void Fire(Vector3 position, Quaternion rotation, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, position, rotation);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector3.up * projectileSpeed;
        Debug.Log($"Projectile speed set to: {projectile.GetComponent<Rigidbody2D>().linearVelocity}. Should be {position * projectileSpeed}");
        if (firingSFX != null)
            PlaySFX(firingSFX, position);
        else Debug.LogWarning("No firing sound effect assigned.");
    }
}

public class LaserWeapon : Weapons
{
    public LaserWeapon(GameObject prefab, float projectileSpeed = 100f) 
    {
        damage = 1;
        this.projectileSpeed = projectileSpeed;
        fireRate = 0.1f;
        projectilePrefab = prefab;
    }
    public override void Fire(Transform firePoint, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = firePoint.up * projectileSpeed;
        PlaySFX(firingSFX, firePoint.position);
    }

    public override void Fire(Vector3 position, Quaternion rotation, AudioClip firingSFX)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, position, rotation);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = rotation * Vector3.up * projectileSpeed;
        Debug.Log($"Projectile speed set to: {projectile.GetComponent<Rigidbody2D>().linearVelocity}. Should be {position * projectileSpeed}");
        if (firingSFX != null)
            PlaySFX(firingSFX, position);
        else Debug.LogWarning("No firing sound effect assigned.");
    }
}

public class HomingBehavior : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;
    private Rigidbody2D rb;
    private float rotationSpeed = 200f;

    public void Initialize(Vector3 targetPosition, float speed, Rigidbody2D rb)
    {
        this.targetPosition = targetPosition;
        this.speed = speed;
        this.rb = rb;
    }

    private void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.linearVelocity = transform.up * speed;
    }
}