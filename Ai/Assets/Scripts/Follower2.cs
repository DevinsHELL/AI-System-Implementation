using UnityEngine;
using UnityEngine.AI;

public class SecondAIFollower : MonoBehaviour
{
    public Transform[] pathPoints;
    public float arrivalDistance = 0.5f;

    private NavMeshAgent navAgent;
    private int targetWaypoint = 0;

    public VisionTracker vision;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        if (pathPoints.Length > 0)
        {
            navAgent.SetDestination(pathPoints[0].position);
        }
    }

    void Update()
    {
        if (vision != null && vision.canSeeTarget && vision.target != null)
        {
            navAgent.SetDestination(vision.target.position); // chase the target
            return;
        }

        if (pathPoints.Length == 0) return;

        if (!navAgent.pathPending && navAgent.remainingDistance < arrivalDistance)
        {
            targetWaypoint = (targetWaypoint + 1) % pathPoints.Length;
            navAgent.SetDestination(pathPoints[targetWaypoint].position);
        }
    }
}

