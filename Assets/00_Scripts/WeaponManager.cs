using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Stats stats;

    public Transform[] machinegunFirePoints;
    public Transform[] secondaryFirePoints;
    public GameObject machinegunBulletPrefab;
    public GameObject missilePrefab;
    public GameObject homingMissilePrefab;
    public GameObject powerMissilePrefab;
    public GameObject primaryMuzzleFlashPrefab;
    public GameObject secondaryMuzzleFlashPrefab;

    public AudioClip primaryProjectileAudioClip;
    public AudioClip secondaryProjectileAudioClip;
    private AudioSource weaponAudioSource;
    //public AudioSource primaryProjectileAudioSource;
    //public AudioSource secondaryProjectileAudioSource;


    float nextFireTime;
    public float primaryFireRate;
    public float secondaryFireRate;
    bool canFire = true;

    public bool isPlayer = true;
    //bool isActive;

    int currentFirePointIndex = 0;
    int currentSecondaryFirePointIndex = 0;

    private void Start()
    {
        stats = GetComponent<Stats>();
        weaponAudioSource = GetComponent<AudioSource>();

        //Setup AudioSources for 3D sound
        weaponAudioSource.spatialBlend = 1f;
        //primaryProjectileAudioSource.spatialBlend = 1f;
        //secondaryProjectileAudioSource.spatialBlend = 1f;

        //primaryProjectileAudioClip = primaryProjectileAudioSource.clip;
        //secondaryProjectileAudioClip = secondaryProjectileAudioSource.clip;
    }

    void Update()
    {
        if (isPlayer)
        {
            if (Input.GetButton("Fire1"))
            {
                Fire1();
            }

            if (Input.GetButton("Fire2"))
            {
                Fire2();
            }
        }
    }

    public void Fire1()
    {
        if (canFire && Time.time > nextFireTime)
        {
            PlayPrimaryOneShot();

            nextFireTime = Time.time + primaryFireRate;

            Instantiate(primaryMuzzleFlashPrefab, machinegunFirePoints[currentFirePointIndex].position, machinegunFirePoints[currentFirePointIndex].rotation);
            Instantiate(machinegunBulletPrefab, machinegunFirePoints[currentFirePointIndex].position, machinegunFirePoints[currentFirePointIndex].rotation);
            currentFirePointIndex = (currentFirePointIndex + 1) % machinegunFirePoints.Length; // Increment the fire point index and wrap around to the beginning if necessary
        }
    }

    public void Fire2()
    {
        if (canFire && Time.time > nextFireTime)
        {
            PlaySecondaryOneShot();

            nextFireTime = Time.time + secondaryFireRate;

            Instantiate(secondaryMuzzleFlashPrefab, secondaryFirePoints[currentSecondaryFirePointIndex].position, secondaryFirePoints[currentSecondaryFirePointIndex].rotation);
            Instantiate(missilePrefab, secondaryFirePoints[currentSecondaryFirePointIndex].position, secondaryFirePoints[currentSecondaryFirePointIndex].rotation);
            currentSecondaryFirePointIndex = (currentSecondaryFirePointIndex + 1) % secondaryFirePoints.Length; // Increment the fire point index and wrap around to the beginning if necessary
        }
    }

    void PlayPrimaryOneShot()
    {
        weaponAudioSource.PlayOneShot(primaryProjectileAudioClip);
        //AudioSource.PlayClipAtPoint(primaryProjectileAudioSource.clip, transform.position);
        //primaryProjectileAudioSource.Play();

    }

    void PlaySecondaryOneShot()
    {
        weaponAudioSource.PlayOneShot(secondaryProjectileAudioClip);
        //AudioSource.PlayClipAtPoint(secondaryProjectileAudioSource.clip, transform.position);
        //primaryProjectileAudioSource.Play();


    }
}
