using UnityEngine;

[System.Serializable]
public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health = 1;
    public float explosionDuration = 1f;

    [Header("Projectile Settings")]
    public GameObject defaultWeaponPrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireCooldown = 3f;

    [Header("Audio Clips")]
    public AudioClip deathSFX;
    public AudioClip firingSFX;
    public AudioClip damageSFX;

    [Range(0, 1)] public float deathVolume = 0.7f;
    [Range(0, 1)] public float firingVolume = 0.7f;
    [Range(0, 1)] public float damageVolume = 0.7f;

    [Header("Pathing")]
    public WaveConfig waveConfig;
}
