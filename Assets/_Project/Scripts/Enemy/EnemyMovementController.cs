using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyController enemyController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (enemyController.PlayerController == null) return;
        if (enemyController.enemyType == EnemyType.Moving)
            agent.destination = enemyController.PlayerController.transform.position;
    }
}
