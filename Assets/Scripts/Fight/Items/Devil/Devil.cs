using UnityEngine;
using System.Collections; // Required for Coroutines

public class Devil : MonoBehaviour
{
    public float despawnTime = 5f; // Time in seconds before the Devil disappears if not collected
    public int damageAmount = 3; // The amount to increase AttackerBallPower by
    private bool triggered;
    public float fallSpeed = 3;

    private Coroutine despawnCoroutine; // Reference to control the despawn coroutine
    
    void FixedUpdate() {
        // Make the Devil fall down at a constant speed
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Starts the countdown timer for the Devil to disappear.
    /// This method should be called right after the Devil is spawned.
    /// </summary>
    public void StartDespawnTimer()
    {
        // Stop any existing despawn coroutine to prevent issues if called multiple times
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
        }

        // Start the despawn countdown coroutine
        despawnCoroutine = StartCoroutine(DespawnRoutine());
        Debug.Log(gameObject.name + " despawn timer started. Will disappear in " + despawnTime + " seconds.");
    }

    IEnumerator DespawnRoutine()
    {
        yield return new WaitForSeconds(despawnTime); // Wait for the defined despawn time

        // If this point is reached, the Devil was not collected in time
        Debug.Log(gameObject.name + " was not collected in time and disappeared.");
        Destroy(gameObject); // Destroy the Devil GameObject
    }

    /// <summary>
    /// Handles collection when another collider enters the Devil's trigger area.
    /// </summary>
    /// <param name="other">The Collider2D of the other object.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the Paddle
        // Assuming your Paddle GameObject has the tag "Paddle"
        if(this.triggered == true) {
            return;
        }
        if (other.name == "Paddle")
        {

            Debug.Log("Paddle collected " + gameObject.name);
            Destroy(gameObject);

            // Stop the despawn timer as the Devil has been collected
            if (despawnCoroutine != null)
            {
                StopCoroutine(despawnCoroutine);
            }

            // --- Find and update AttackerBall's power ---
            // 1. Find the AttackerBall object in the scene.
            //    It's crucial that there is only one AttackerBall, or you need a way to identify the correct one.
            GameObject attackerBallObject = GameObject.Find("AttackerBall"); // Assumes AttackerBall's name is exactly "AttackerBall"
            if(this.triggered == false) attackerBallObject.GetComponent<AttackerBallInitializer>().Point -= 500; // Decrease the score of the Paddle
            if(this.triggered == false) attackerBallObject.GetComponent<AttackerBallInitializer>().Bonus /= 2;
            if(this.triggered == false) attackerBallObject.GetComponent<AttackerBallInitializer>().AttackerBallPower /= 2;
            if(attackerBallObject.GetComponent<AttackerBallInitializer>().Bonus < 1) attackerBallObject.GetComponent<AttackerBallInitializer>().Bonus = 1;
            if(attackerBallObject.GetComponent<AttackerBallInitializer>().AttackerBallPower < 1) attackerBallObject.GetComponent<AttackerBallInitializer>().AttackerBallPower = 1;
            this.triggered = true;
            Destroy(gameObject); // Destroy the Devil GameObject upon collection
            this.triggered = false;
        }
        else
        {
            // Optional: Log if something else triggers the Devil
            Debug.Log($"Devil triggered by {other.gameObject.name} (Tag: {other.tag}), but not collected.");
        }
    }

    void OnDisable()
    {
        // Ensure the coroutine is stopped if the GameObject becomes inactive or is destroyed
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
        }
    }
}