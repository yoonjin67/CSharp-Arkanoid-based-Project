using UnityEngine;

public class ManageBlockCollision_Boss : MonoBehaviour
{
    private int RemainingPower = 50;
    public AudioClip physicalHitSound;
    private int BallPower = 3;

    AudioSource sfxAudioSource; // For sound effects
    
    bool isHit = false;
    public SharedIntVariable counter;
    void Awake()
    {
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
            collidedGameObject.GetComponent<AttackerBallInitializer>().Point += 7 * collidedGameObject.GetComponent<AttackerBallInitializer>().Bonus;    
            Debug.Log($"Remaining Power: {this.RemainingPower}");
            this.isHit = false; // Reset the flag after processing the hit   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        Debug.Log($"Remaining Power: {this.RemainingPower}");

        if (collidedGameObject.name == "AttackerBall")
        {
            if(physicalHitSound != null) 
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
