using UnityEngine;
using UnityEngine.AI;

public class Seeker_Vision : MonoBehaviour
{
    public VisionTracker vision; // Reference to VisionTracker
    private NavMeshAgent navAgent;
    private Renderer aiRenderer;
    private bool isChasing = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aiRenderer = GetComponent<Renderer>();

        navAgent.isStopped = true; // Starts idle
        aiRenderer.material.color = Color.white;
    }

    void Update()
    {
        if (vision != null && vision.target != null)
        {
            if (vision.canSeeTarget)
            {
                // Starts chase sequence
                if (!isChasing)
                {
                    isChasing = true;
                    navAgent.isStopped = false;
                    aiRenderer.material.color = Color.red;
                }

                navAgent.SetDestination(vision.target.position);
            }
            else
            {
                // if the ai loses sight of the one its tracking it stops and returns to its idle state
                if (isChasing)
                {
                    isChasing = false;
                    navAgent.isStopped = true;
                    aiRenderer.material.color = Color.white;
                }
            }
        }
    }
}


