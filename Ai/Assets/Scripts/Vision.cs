using UnityEngine;

public class VisionTracker : MonoBehaviour
{
    public Transform target;
    public float viewRadius = 10f;
    public float viewAngle = 60f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    [HideInInspector]
    public bool canSeeTarget;

    void Update()
    {
        DetectTarget();
    }

    void DetectTarget()
    {
        canSeeTarget = false;

        if (target == null) return;

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2 && distanceToTarget < viewRadius)
        {
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                canSeeTarget = true;
            }
        }
    }

     // show vision cone
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 left = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, left * viewRadius);
        Gizmos.DrawRay(transform.position, right * viewRadius);
    }
}

