using UnityEngine;

public class ManageBlockCollision_DevGirl : MonoBehaviour
{
    private int RemainingPower = 6;
    public AudioClip physicalHitSound;
    private int BallPower = 3;
    public SharedIntVariable counter;
    bool isHit = false;
    AudioSource sfxAudioSource; // For sound effects
    void Awake()
    {
        // Check if a GameObject for sound effects has been assigned in the Inspector.

        this.sfxAudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(isHit) {
            return;
        }
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.name != "AttackerBall")
        {
            this.isHit = true;
            this.BallPower = collidedGameObject.GetComponent<AttackerBallInitializer>().AttackerBallPower;
            collidedGameObject.GetComponent<AttackerBallInitializer>().Point += 6 * collidedGameObject.GetComponent<AttackerBallInitializer>().Bonus;    
            this.RemainingPower -= this.BallPower;
            Debug.Log($"Remaining Power: {this.RemainingPower}");   
            this.isHit = false; // Reset the flag after processing the hit   
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        GameObject collidedGameObject = collision.gameObject;
        Debug.Log($"Remaining Power: {this.RemainingPower}");
        if(collidedGameObject.name == "AttackerBall") {
            if (physicalHitSound != null)
            {
                sfxAudioSource.PlayOneShot(physicalHitSound);
            }
            if(this.RemainingPower <= 0) {
                Destroy(gameObject);
                counter.value--;
            }
        }
    }
}
