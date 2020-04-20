using UnityEngine;
using UnityEngine.AI;
public class Car : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (PauseManager.GamePaused)
            agent.isStopped = true;
        else
            agent.isStopped = false;

        float distance = (agent.destination - transform.position).sqrMagnitude;

        if (distance < 1.5)
        {
            Destroy(gameObject);
        }
    }
}
