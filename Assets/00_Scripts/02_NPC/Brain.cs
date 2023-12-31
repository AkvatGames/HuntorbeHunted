using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    private Eyes eyes;
    private Ears ears;
    private Weapon weapon;

    public bool targetDetected;
    public bool isPlayer;

    private NavMeshAgent agent;

    Vector3 destination;
    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        //steup the AI's senses (seeing, hearing)
        eyes = GetComponentInChildren<Eyes>();
        ears = GetComponentInChildren<Ears>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (eyes != null)
        {
            if (eyes.targetSeen || ears.targetHeard)
            {
                //a target has been detected!!!
                targetDetected = true;
            }
            else if (!eyes.targetSeen)
            {
                targetDetected = false;
            }
        }

        

        if (Vector3.Distance(destination, playerObject.transform.position) > 1.0f)
        {
            destination = playerObject.transform.position;
        }
    }

    void SetTarget()
    {

    }

    void Idle()
    {
        Debug.Log("Idle!");
    }

    void MoveTowards()
    {
        agent.destination = playerObject.transform.position;
    }

    void Patrol()
    {

    }

    void Attack()
    {
        if (weapon != null)
        {
            if (targetDetected && agent.remainingDistance <= agent.stoppingDistance)
            {
                weapon.Fire();
                Debug.Log("Attack!");
            }
        }
        else if (weapon == null)
        {
            // setup melee attack here
        }

    }
}
