using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletSpeed = 345;
    public float hitForce = 50f;
    public float destroyAfter = 3.5f;
    RaycastHit hit;

    public bool HeatSeeker;

    public float projectileDamage = 10f;

    public float damageRadius = 5f;

    public GameObject hitParticle;

    public AudioClip hitSound;

    Player player;
    Enemy enemy;

    float currentTime = 0;
    Vector3 newPos;
    Vector3 oldPos;
    bool hasHit = false;

    bool isPlayer;

    //public GameObject enemyHitParticle;

    // Start is called before the first frame update
    IEnumerator Start()
    {


        player = FindAnyObjectByType<Player>();
        enemy = FindAnyObjectByType<Enemy>();

        newPos = transform.position;
        oldPos = newPos;

        while (currentTime < destroyAfter && !hasHit)
        {
            Vector3 velocity = transform.forward * bulletSpeed;
            newPos += velocity * Time.deltaTime;
            Vector3 direction = newPos - oldPos;
            float distance = direction.magnitude;
            gameObject.layer = LayerMask.NameToLayer("Explosion");


            // Check if we hit anything on the way
            if (Physics.Raycast(oldPos, direction, out hit, distance))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(direction * hitForce);

                    DamageReceiver damageReceiver = hit.transform.GetComponent<DamageReceiver>();
                    if (damageReceiver != null)
                    {
                        //Instantiate(hitParticle, hit.point, Quaternion.identity);
                        StartCoroutine(DestroyBullet());
                    }
                }


                newPos = hit.point; //Adjust new position


                StartCoroutine(DestroyBullet());
            }

            currentTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();

            transform.position = newPos;
            oldPos = newPos;
        }

        if (!hasHit)
        {
            //Instantiate(explosion, hit.point, Quaternion.identity);
            //Instantiate(explosion, transform.position, Quaternion.identity);

            StartCoroutine(DestroyBullet());
        }
    }

    private void Update()
    {
        if (HeatSeeker && gameObject.GetComponent<Player>())
        {
            RotateTowardsTarget(enemy.transform);
        }
    }

    void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float rotationSpeed = 5f; // Adjust the rotation speed as desired

        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        AreaDamageEnemies(other.transform.position, damageRadius, projectileDamage);
        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Instantiate(hitParticle, hit.point, Quaternion.identity);
        hasHit = true;
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider other in objectsInRange)
        {
            DamageReceiver damageReceiver = other.gameObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
            {
                float proximity = (location - damageReceiver.transform.position).magnitude;
                float effect = 1 - (proximity / radius);
                effect = Mathf.Clamp01(effect);  // Ensure the effect is within the range of [0, 1]
                damageReceiver.ApplyDamage(damage * effect);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}