using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private PlayerMovementController playerMovementController;
    private EnemyController enemyController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        playerMovementController = FindFirstObjectByType<PlayerMovementController>();
    }

    void Update()
    {
        if(enemyController.enemyType == EnemyType.Moving)
            agent.destination = playerMovementController.transform.position;
    }
}
