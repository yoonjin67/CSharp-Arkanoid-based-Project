using UnityEngine;
using System.Collections; // Required for Coroutines

public class DevilSpawner : MonoBehaviour
{
    public GameObject devilPrefab; // Assign your Devil Prefab here in the Inspector
    public float spawnInterval = 25f; // Time in seconds between Devil spawns
    private Vector3 spawnPoint; // Optional: Assign a specific transform for spawning. If null, spawns at spawner's position.

    private Coroutine spawnCoroutine; // Reference to control the spawning coroutine

    void Start()
    {
        spawnPoint = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0); // Random spawn point within a range
        // Start the coroutine that periodically spawns Devils
        spawnCoroutine = StartCoroutine(SpawnDevilRoutine());
    }

    IEnumerator SpawnDevilRoutine() {
    
        while (true) // Loop indefinitely to keep spawning Devils
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the defined spawn interval

            // Spawn the Devil
            
            SpawnDevil();
        }
    }

    void SpawnDevil()
    {
        if (devilPrefab != null)
        {
            // Determine the actual spawn position
            spawnPoint = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0); // Random spawn point within a range
            Vector3 actualSpawnPos = spawnPoint;
            
            // Instantiate the Devil
            GameObject newDevil = Instantiate(devilPrefab, actualSpawnPos, Quaternion.identity);
            Debug.Log("Devil spawned at: " + actualSpawnPos);

            // Get the Devil script from the newly created item and start its despawn timer
            Devil devilScript = newDevil.GetComponent<Devil>();
            if (devilScript != null)
            {
                devilScript.StartDespawnTimer(); // Call the public method to begin the despawn countdown
            }
            else
            {
                Debug.LogWarning("Spawned Devil prefab does not have a 'Devil.cs' script attached!");
            }
        }
        else
        {
            Debug.LogError("Devil Prefab is not assigned in the DevilSpawner script!");
        }
    }

    void OnDisable()
    {
        // When the spawner GameObject is disabled or destroyed, stop the coroutine
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}