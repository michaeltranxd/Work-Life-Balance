using UnityEngine;
using UnityEngine.AI;
public class AIMoveTo : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    void Update()
    {
        //transform.LookAt(agent.nextPosition);
    }
}
