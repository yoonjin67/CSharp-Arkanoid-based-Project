using UnityEngine;
using TMPro;

public class AttackerBallInitializer : MonoBehaviour
{
    float POW(float v) {
        return v * v;
    } // no need to import Mathf for this

    public float upwardForce = 2f; // Force strength applied upward
    public int Point = 0;
    public int Bonus = 1;
    public float accelerationDuration = 0.2f; // Duration to apply upward force
    public int AttackerBallPower = 3;

    private Rigidbody2D rb;
    private float timer = 0f;
    private bool hasAccelerated = false;

    public TextMeshProUGUI infoLabel; // UI label to show Point and Bonus
    public GameObject background; // Reference to background GameObject (not parent)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody 2D component not found on this GameObject!");
        }

        UpdateUILabel(); // Initialize label text
    }

    void ResetPosition()
    {
        transform.position = new Vector2(0, -3.2f);
        rb.linearVelocity = Vector2.zero;
        rb.position = new Vector2(0, -3.2f);
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        if ((rb.position.y >= 2.0f && Mathf.Abs(rb.linearVelocity.y) <= 0.1f) || Mathf.Abs(rb.linearVelocity.sqrMagnitude) < 0.1f)
        {
            ResetPosition();
        }

        if (!hasAccelerated)
        {
            timer += Time.fixedDeltaTime;
            if (timer < accelerationDuration)
            {
                rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
            }
            else
            {
                hasAccelerated = true;
            }
        }
        UpdateUILabel();
    }

    void UpdateUILabel()
    {
        if (infoLabel != null)
        {
            infoLabel.text = $"<b><color=yellow>Point:</color></b> {Point:N0} <b><color=green> BallPower:</color></b> {AttackerBallPower:N0}\n<b><color=red> Bonus: </color></b> x{Bonus}";
        }
    }
}
