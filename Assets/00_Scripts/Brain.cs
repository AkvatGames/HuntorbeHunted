using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    private Eyes eyes;
    private Ears ears;
    private WeaponManager weaponManager;

    public bool targetDetected;
    public bool isPlayer;

    Vector3 destination;
    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        //steup the AI's senses (seeing, hearing)
        //agent = GetComponent<NavMeshAgent>();
        eyes = GetComponentInChildren<Eyes>();
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.isVisible | player.isAudible)
        //{
        if (eyes != null)
        {
            if (eyes.targetSeen)// | ears.targetHeard)
            {
                //a target has been detected!!!
                targetDetected = true;
            }
            else if (!eyes.targetSeen)
            {
                targetDetected = false;
            }
        }


        if (Input.GetKey(KeyCode.Space) && !isPlayer)// && eyes.targetSeen)// && targetDetected)
        {
            //isActive = true;
            //transform.LookAt(playerObject.transform.position);
            weaponManager.Fire2();
        }
        else// if (!isPlayer)// && targetDetected)
        {
            weaponManager.Fire1();
        }
        //}


        if (targetDetected)
        {

        }

        if (!targetDetected)
        {

        }

        if (Vector3.Distance(destination, playerObject.transform.position) > 1.0f)
        {
            destination = playerObject.transform.position;
        }
    }

    void Idle()
    {
        Debug.Log("Idle!");
    }

    void MoveTowards()
    {
        //agent.destination = playerObject.transform.position;
    }

    void Patrol()
    {

    }

    void Attack()
    {
        Debug.Log("Attack!");

    }
}
