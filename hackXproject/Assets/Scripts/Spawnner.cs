using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs; // List of GameObject prefabs to spawn
    [SerializeField] private List<Transform> spawnPoints;
    public Transform quizParent;
    public float spawnInterval = 5f; // Time interval between each spawn

    [SerializeField] private float spawnTimer = 0f; // Timer for spawning


    public void spawnnerSpawn()
    {
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn
        if (spawnTimer > spawnInterval)
        {
            // Reset the timer
            spawn();
            spawnTimer = 0f;
        }
    }

    public void spawn()
    {      
        int index = Random.Range(0, objectPrefabs.Count);
        GameObject objectToSpawn = objectPrefabs[index];
        Vector3 positionToSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        GameObject quiz = Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity, quizParent);
    }
}
