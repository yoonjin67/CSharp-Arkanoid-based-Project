using UnityEngine;

public class ManageBlockCollision_DevGuy : MonoBehaviour
{
    private int RemainingPower = 5;
    public AudioClip physicalHitSound;
    AudioSource sfxAudioSource; // For sound effects
    public GameObject soundEffectGameObject;
    private int BallPower = 3;
    bool isHit = false;
    public SharedIntVariable counter;

    void Awake()

    {
    // Check if a GameObject for sound effects has been assigned in the Inspector.
        // Check if a GameObject for sound effects has been assigned in the Inspector.

        this.sfxAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(this.isHit) {
            return;
        }
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.name == "AttackerBall")
        {
            this.isHit = true;
            this.BallPower = collidedGameObject.GetComponent<AttackerBallInitializer>().AttackerBallPower;
            this.RemainingPower -= this.BallPower;
            collidedGameObject.GetComponent<AttackerBallInitializer>().Point += 5* collidedGameObject.GetComponent<AttackerBallInitializer>().Bonus;    
            Debug.Log($"Remaining Power: {this.RemainingPower}");
            this.isHit = false; // Reset the flag after processing the hit   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        Debug.Log($"Remaining Power: {this.RemainingPower}");
        Debug.Log($"Remaining Power: {this.RemainingPower}");
        if (collidedGameObject.name == "AttackerBall")
        {
            if (physicalHitSound != null)
            {
                sfxAudioSource.PlayOneShot(physicalHitSound);
                Debug.Log("play sound");
            }
            if(this.RemainingPower <= 0) {
                Destroy(gameObject);
                counter.value--;
            }
        }
    }
}
