using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Space(5)]
    [Header("Weapon Stats")]
    public float weaponDamage;
    public float prjectileSpeed;
    public float reloadTimer;

    public Transform[] firePoints;
    public Projectile projectilePrefab;
    public ParticleSystem muzzleFlash;

    public AudioClip weaponAudioClip;
    private AudioSource weaponAudioSource;

    float nextFireTime;
    public float primaryFireRate;
    public float secondaryFireRate;
    bool canFire = true;

    int currentFirePointIndex = 0;

    private void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (canFire && Time.time > nextFireTime)
        {
            PlayPrimaryOneShot();

            nextFireTime = Time.time + primaryFireRate;

            Instantiate(muzzleFlash, firePoints[currentFirePointIndex].position, firePoints[currentFirePointIndex].rotation);
            Instantiate(projectilePrefab, firePoints[currentFirePointIndex].position, firePoints[currentFirePointIndex].rotation);
            currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Length; // Increment the fire point index and wrap around to the beginning if necessary
        }
    }

    void Reload()
    {
        canFire = false;
    }

    void PlayPrimaryOneShot()
    {
        weaponAudioSource.PlayOneShot(weaponAudioClip);
    }
}
