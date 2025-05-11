using UnityEngine;
using System.Collections;

public class EnemyFiring : MonoBehaviour
{
    private EnemyStats stats;
    private EnemyAnimationManager animationManager;
    private float cooldownTimer;
    private bool isCharging;
    private bool canFire = false;

    private Weapons currentWeapon;

    public void Initialize(EnemyStats stats, EnemyAnimationManager animationManager)
    {
        this.stats = stats;
        this.animationManager = animationManager;

        cooldownTimer = stats.fireCooldown;
        if (currentWeapon == null)
        {
            currentWeapon = new DefaultWeapon(stats.defaultWeaponPrefab, stats.firePoint, stats.projectileSpeed);
        }
    }

    public void HandleFiring()
    {
        if (cooldownTimer < 0f && !isCharging && canFire) StartCoroutine(FireSequence());
        else if (cooldownTimer <= 0f) canFire = true;
        else cooldownTimer -= Time.deltaTime;
    }

    private IEnumerator FireSequence()
    {
        canFire = false;
        isCharging = true;

        animationManager?.StartChargingAnimation();

        yield return new WaitForSeconds(stats.fireCooldown);

        animationManager?.StopChargingAnimation();

        if(stats.defaultWeaponPrefab != null && stats.firePoint != null)
        {
            if (currentWeapon == null)
            {
                currentWeapon = new DefaultWeapon(stats.defaultWeaponPrefab, stats.firePoint, stats.projectileSpeed);
            }
            Vector3 offsetPosition = stats.firePoint.position + stats.firePoint.up * 1.5f;
            currentWeapon.Fire(offsetPosition, stats.firePoint.rotation, stats.firingSFX);
        }

        cooldownTimer = stats.fireCooldown;
        isCharging = false;
        canFire = true;
    }
}
