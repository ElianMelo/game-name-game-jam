using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerMovementController playerMovementController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerMovementController = FindFirstObjectByType<PlayerMovementController>();
    }

    void Update()
    {
        agent.destination = playerMovementController.transform.position;
    }
}
