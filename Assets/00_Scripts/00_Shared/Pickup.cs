using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject pickupPrefab;

    //Bools setting up different pickup types
    public bool healthPickup;
    public bool weaponPickup;

    public float healAmount;

    public float respawnTimer;

    bool collectable = true;

    private void OnTriggerEnter(Collider other)
    {
        Stats stats = other.GetComponent<Stats>();

        if (stats != null)
        {
            if (collectable && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")))
            {
                //Do all the different pickup stuff here
                if (healthPickup && stats.currentHealth < stats.maximumHealth)
                {
                    stats.currentHealth += healAmount;

                    if (stats.currentHealth > stats.maximumHealth)
                    {
                        stats.currentHealth = stats.maximumHealth;
                    }

                    StartCoroutine(Collected());
                }
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator Collected()
    {
        collectable = false;
        pickupPrefab.SetActive(false);
        yield return new WaitForSeconds(respawnTimer);
        pickupPrefab.SetActive(true);
        collectable = true;
    }
}
