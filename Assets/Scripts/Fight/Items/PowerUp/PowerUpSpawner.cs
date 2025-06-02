using UnityEngine;
using System.Collections; // Required for Coroutines

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Assign your PowerUp Prefab here in the Inspector
    public float spawnInterval = 25f; // Time in seconds between PowerUp spawns
    private Vector3 spawnPoint; // Optional: Assign a specific transform for spawning. If null, spawns at spawner's position.

    private Coroutine spawnCoroutine; // Reference to control the spawning coroutine

    void Start()
    {
        spawnPoint = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0); // Random spawn point within a range
        // Start the coroutine that periodically spawns PowerUps
        spawnCoroutine = StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnPowerUpRoutine() {
    
        while (true) // Loop indefinitely to keep spawning PowerUps
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for the defined spawn interval

            // Spawn the PowerUp
            
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefab != null)
        {
            // Determine the actual spawn position
            Vector3 actualSpawnPos = spawnPoint;
            
            // Instantiate the PowerUp
            GameObject newPowerUp = Instantiate(powerUpPrefab, actualSpawnPos, Quaternion.identity);
            Debug.Log("PowerUp spawned at: " + actualSpawnPos);

            // Get the PowerUp script from the newly created item and start its despawn timer
            PowerUp powerUpScript = newPowerUp.GetComponent<PowerUp>();
            if (powerUpScript != null)
            {
                powerUpScript.StartDespawnTimer(); // Call the public method to begin the despawn countdown
            }
            else
            {
                Debug.LogWarning("Spawned PowerUp prefab does not have a 'PowerUp.cs' script attached!");
            }
        }
        else
        {
            Debug.LogError("PowerUp Prefab is not assigned in the PowerUpSpawner script!");
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