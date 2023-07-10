using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [Space(5)]
    [Header("References")]
    private Stats stats;
    public ParticleSystem hitParticles;
    public ParticleSystem deathParticles;
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
            Instantiate(hitParticles, transform.position, transform.rotation);
            Debug.Log("Health:" + stats.currentHealth);
        }
        else if (stats.currentHealth <= 0)
        {
            PlayDeathAudioClip();
            Instantiate(deathParticles, transform.position, transform.rotation);
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
