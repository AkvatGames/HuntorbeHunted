using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    private Stats stats;

    [Space(5)]
    [Header("References")]
    public GameObject hitParticlesPrefab;
    public GameObject deathParticlesPrefab;
    public AudioClip hitAudioClip;
    public AudioClip deathAudioClip;

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }

    public void ApplyDamage(float points)
    {
        stats.currentHealth -= points;

        if (stats.currentHealth > 0)
        {
            PlayHitAudioClip();
            Instantiate(hitParticlesPrefab, transform.position, transform.rotation);
            Debug.Log("Health:" + stats.currentHealth);
        }
        else if (stats.currentHealth <= 0)
        {
            PlayDeathAudioClip();
            Instantiate(deathParticlesPrefab, transform.position, transform.rotation);
            stats.currentHealth = 0;
            Debug.Log("DEAD!!!");
            Destroy(gameObject, 0.05f);
        }

        Debug.Log(this.gameObject.name + this.stats.currentHealth);
    }

    void PlayHitAudioClip()
    {
        AudioSource.PlayClipAtPoint(hitAudioClip, transform.position);
    }

    void PlayDeathAudioClip()
    {
        AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);
    }
}
