using UnityEngine;
using UnityEngine.AI;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints;
    public float stoppingDistance = 0.5f;

    private NavMeshAgent navAgent;
    private int currentWaypointIndex = 0;

    public VisionTracker vision; // Reference to the vision script

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            navAgent.SetDestination(waypoints[0].position);
        }
    }

    void Update()
    {
        if (vision != null && vision.canSeeTarget && vision.target != null)
        {
            navAgent.SetDestination(vision.target.position); // chase the target
            return;
        }

        if (waypoints.Length == 0) return;

        if (!navAgent.pathPending && navAgent.remainingDistance < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            navAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}

