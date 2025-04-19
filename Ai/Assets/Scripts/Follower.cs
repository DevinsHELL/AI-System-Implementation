using UnityEngine;
using UnityEngine.AI;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints; // public array to add way points for each follower path
    public float stoppingDistance = 0.5f; // the amount over a waypoint that the ai registers as crossed. A buffer to make sure that the ai ,akes it to a said way point


    private NavMeshAgent navAgent;
    private int currentWaypointIndex = 0;

    public VisionTracker vision; // Reference to the vision script

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); //grabs nav mesh 

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

