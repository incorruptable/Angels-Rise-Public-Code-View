using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private EnemyAnimationManager animationManager;
    private EnemyStats stats;

    public bool IsDead => currentHealth <= 0;

    public void Initialize(EnemyStats stats, EnemyAnimationManager animationManager)
    {
        this.stats = stats;
        this.animationManager = animationManager;
        currentHealth = stats.health;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            if (stats.damageSFX != null)
                AudioSource.PlayClipAtPoint(stats.damageSFX, Camera.main.transform.position, stats.damageVolume);
        }
        else Die();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null) TakeDamage(damageDealer.getDamage());
    }

    private void Die()
    {
        if (animationManager != null) animationManager.PlayDeathAnimation();
        if (stats.deathSFX != null) AudioSource.PlayClipAtPoint(stats.deathSFX, Camera.main.transform.position, stats.deathVolume);

        Destroy(gameObject, stats.explosionDuration);
    }
}
