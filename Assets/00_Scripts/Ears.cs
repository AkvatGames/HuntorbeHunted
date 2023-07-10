using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ears : MonoBehaviour
{

    public int hearingRange;
    public bool hasEars = true;
    public bool canHear = true;
    public bool targetHeard;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEars && canHear)
        {
            CheckIfPlayerIsInRange();
        }
    }

    void CheckIfPlayerIsInRange()
    {
        // Get the direction to the player
        Vector3 directionToPlayer = GetDirectionToPlayer();

        // Check if the player is within the detection range and angle
        bool withinRange = directionToPlayer.magnitude <= hearingRange;

        GameObject player = GameObject.FindGameObjectWithTag("Player");


        if (withinRange)// && player.gameObject.GetComponent<PlayerController>().isAudible)
        {
            // Player is within range and angle
            targetHeard = true;
            Debug.Log("Player Heard!");
        }
        else
        {
            targetHeard = false;
        }
    }

    private Vector3 GetDirectionToPlayer()
    {
        // Get the direction from this GameObject to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position - transform.position;
        }

        return Vector3.zero;
    }

    private void DrawHearingSphere()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }

    private void OnDrawGizmos()
    {
        // Draw the hearing range sphere
        DrawHearingSphere();

    }
}
