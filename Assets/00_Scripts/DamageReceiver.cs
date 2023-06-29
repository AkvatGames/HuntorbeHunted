using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [Space(5)]
    [Header("References")]
    Stats stats;
    public GameObject deathExplosionParticle;
    //public AudioSource deathAudioSource;
    public AudioClip deathAudioClip;

    public int maximumLives;

    public float respawnTimer;
    //Player player;
    //Enemy enemy;

    private void Start()
    {
        stats = GetComponent<Stats>();
        //player = FindAnyObjectByType<Player>();
    }

    public void ApplyDamage(float points)
    {
        /*
        if (stats.currentShield < 0)
        {
            float additionalDamage = Mathf.Abs(stats.currentShield);
            //float additionalArmourDamage = Mathf.Abs(stats.currentArmour);
            if (stats.currentShield <= 0)
            {
                stats.currentShield -= additionalDamage;
                stats.currentShield = 0;
            }

            if (stats.currentArmour <= 0)
            {
                additionalDamage = Mathf.Abs(stats.currentArmour);

                stats.currentArmour -= additionalDamage;
                stats.currentArmour = 0;
            }
        }
        else if (stats.currentArmour > 0)
        {
            stats.currentArmour -= points;
        }
        else if (stats.currentShield <= 0 || stats.currentArmour <= 0)
        {
            stats.currentHealth -= points;
        }
        */

        stats.currentHealth -= points;

        if (stats.currentHealth <= 0)
        {
            PlayDeathOneShotAudio();
            Instantiate(deathExplosionParticle, transform.position, transform.rotation);
            stats.currentHealth = 0;
            Debug.Log("DEAD!!!");
            //StartCoroutine(Respawn());
            Destroy(gameObject, 0.05f);
            //gameObject.SetActive(false);
        }

        //if (livesRemaining <= 0)
        //{
        //    Debug.Log("FULLY DEAD!!!");
        //    //Destroy(gameObject, 0.05f);
        //    gameObject.SetActive(false);
        //}


        Mathf.Clamp(stats.currentArmour, 0, stats.maximumArmour);
        Mathf.Clamp(stats.currentHealth, 0, stats.maximumHealth);

        Debug.Log(this.gameObject.name + this.stats.currentHealth);



    }

    void PlayDeathOneShotAudio()
    {
        AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);
    }

    IEnumerator Respawn()
    {
        respawnTimer *= Time.deltaTime;
        yield return new WaitForSeconds(respawnTimer);
        Debug.Log(respawnTimer);
    }
}
