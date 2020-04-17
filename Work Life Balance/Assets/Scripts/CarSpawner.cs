using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarSpawner : MonoBehaviour
{
    float time = 0.0f;
    float pastTime = 0.0f;

    float spawnRateInSeconds = 10.0f;
    public GameObject carPrefab;

    public GameObject destinationList;

    Vector3[] destinationPoints;
    Vector3[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize destinationPoints + spawnPoints
        int index = 0;
        spawnPoints = new Vector3[transform.childCount];
        foreach (Transform child in transform)
            spawnPoints[index++] = child.transform.position;

        index = 0;
        destinationPoints = new Vector3[destinationList.transform.childCount];
        foreach (Transform child in destinationList.transform)
            destinationPoints[index++] = child.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if((time - pastTime) > spawnRateInSeconds){
            spawn();
            pastTime = time;
        }
    }

    void spawn()
    {
        // Randomly pick an index of spawn + destination
        int randomDestinationIndex = Random.Range(0, destinationPoints.Length);
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

        // Create car
        GameObject newCar = Instantiate(carPrefab);

        // Assign position and destination to random spawn + destination points defined on map
        newCar.transform.position = spawnPoints[randomSpawnIndex];

        NavMeshAgent agent = newCar.AddComponent<NavMeshAgent>();
        agent.baseOffset = 0.9f;
        agent.destination = destinationPoints[randomDestinationIndex];
    }
}
