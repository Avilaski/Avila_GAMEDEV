using UnityEngine;

public class EyeBotPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float turnSpeed = 180f;

    [Header("Detection")]
    [SerializeField] Light spotLight;
    [SerializeField] Collider playerCollider;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask obstacleLayers;

    Transform targetPoint;
    bool playerDetected;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            enabled = false;
            return;
        }

        transform.position = pointA.position;
        targetPoint = pointB;
    }

    void Update()
    {
        Patrol();

        bool detected = DetectPlayer();

        if (detected && !playerDetected)
        {
            Debug.Log("Detected!");
            PlayerStats stats = playerCollider.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.Respawn();
            } else {
                Debug.LogWarning("Add PlayerStats to the detected PlayerCapsule", this);
            }
        }

        if (!detected && playerDetected)
        {
            Debug.Log("Player escaped detection.");
        }

        playerDetected = detected;
    }

    void Patrol()
    {
        Vector3 direction = targetPoint.position - transform.position;

        if (direction.magnitude < 0.05f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
            return;
        }

        // Turn toward the next point
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Turn before moving
        if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        }
    }

    bool DetectPlayer()
    {
        if (spotLight == null || playerCollider == null)
        {
            return false;
        }

        if (!spotLight.isActiveAndEnabled)
        {
            return false;
        }

        Vector3 origin = spotLight.transform.position;
        float range = spotLight.range;

        if (!Physics.CheckSphere(origin, range, playerLayer, QueryTriggerInteraction.Ignore))
        {
            return false;
        }

        Vector3 toPlayer = playerCollider.bounds.center - origin;
        float distance = toPlayer.magnitude;

        if (distance > range)
        {
            return false;
        }

        float angle = Vector3.Angle(spotLight.transform.forward, toPlayer);

        if (angle > spotLight.spotAngle / 2f)
        {
            return false;
        }

        // Check for walls
        bool blocked = Physics.Raycast(origin, toPlayer.normalized, distance, obstacleLayers, QueryTriggerInteraction.Ignore);

        Debug.DrawRay(origin, toPlayer, blocked ? Color.red : Color.green);

        return !blocked;
    }
}
