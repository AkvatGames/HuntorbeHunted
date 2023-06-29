using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Eyes : MonoBehaviour
{
    public bool targetSeen;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float detection_delay = 0.5f;
    private Collider[] targetColliders;
    private SphereCollider detection_collider;
    private Coroutine detect_player;

    private void Awake()
    {
        detection_collider = GetComponent<SphereCollider>();
        targetColliders = new Collider[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            targetColliders[i] = targets[i].GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (other.gameObject == targets[i])
            {
                detect_player = StartCoroutine(DetectPlayer(i));
                targetColliders[i] = other;
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (other.gameObject == targets[i])
            {
                StopCoroutine(detect_player);
                targetColliders[i] = null;
                // player is hidden
                break;
            }
        }
    }

    IEnumerator DetectPlayer(int targetIndex)
    {
        while (true)
        {
            yield return new WaitForSeconds(detection_delay);
            Vector3[] points = GetBoundingPoints(targetColliders[targetIndex].bounds);
            int points_hidden = 0;
            foreach (Vector3 point in points)
            {
                Vector3 target_direction = point - transform.position;
                float target_distance = Vector3.Distance(transform.position, point);
                float target_angle = Vector3.Angle(target_direction, transform.forward);
                if (IsPointCovered(target_direction, target_distance) || target_angle > 70)
                    ++points_hidden;
            }
            if (points_hidden >= points.Length)
            {
                // player is hidden
                targetSeen = false;
            }
            else
            {
                // player is visible
                targetSeen = true;
            }
        }
    }

    private bool IsPointCovered(Vector3 target_direction, float target_distance)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, target_direction, detection_collider.radius);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Destructible"))
            {
                float cover_distance = Vector3.Distance(transform.position, hit.point);
                if (cover_distance < target_distance)
                    return true;
            }
        }
        return false;
    }

    private Vector3[] GetBoundingPoints(Bounds bounds)
    {
        Vector3[] bounding_points =
        {
            bounds.min,
            bounds.max,
            new Vector3(bounds.min.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.min.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.min.z),
            new Vector3(bounds.min.x, bounds.max.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.min.y, bounds.max.z),
            new Vector3(bounds.max.x, bounds.max.y, bounds.min.z)
        };
        return bounding_points;
    }
}