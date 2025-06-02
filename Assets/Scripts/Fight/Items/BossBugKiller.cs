using UnityEngine;

public class BossBugKiller : MonoBehaviour
{
    public SharedIntVariable counter;
    public int RemainingPower = 50;
    public AudioClip physicalHitSound;
    private int BallPower = 3;
    bool isHit = false;
    AudioSource sfxAudioSource; // For sound effects
    void Awake()
    {
        // Check if a GameObject for sound effects has been assigned in the Inspector.
        this.sfxAudioSource = GetComponent<AudioSource>();
    }
    void Start() {
        this.counter.value ++;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(this.isHit) {
            return;
        }
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.name == "AttackerBall")
        {
            this.isHit = true;
            if(physicalHitSound != null) 
            {
                sfxAudioSource.PlayOneShot(physicalHitSound);
            }
            this.BallPower = collidedGameObject.GetComponent<AttackerBallInitializer>().AttackerBallPower;
            this.RemainingPower -= this.BallPower;
            collidedGameObject.GetComponent<AttackerBallInitializer>().Point += 2000 * collidedGameObject.GetComponent<AttackerBallInitializer>().Bonus;    
            Debug.Log($"Remaining Power: {this.RemainingPower}");
            this.isHit = false; // Reset the flag after processing the hit   
            if(this.RemainingPower <= 0) {
                this.counter.value--;
                Debug.Log("Boss is dead, feel free to shoot!");
                Destroy(gameObject);
            }
        }

    }
}
            