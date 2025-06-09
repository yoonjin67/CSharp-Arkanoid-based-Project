using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float paddleMoveSpeed = 100f; 
    public Rigidbody2D rb;
    private void nullcheck(Object o, string oName) {
        if(o==null) {
            Debug.Log($"Object <{oName}> is null");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        nullcheck(rb, "RigidBody For PaddleController");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateForMobile();
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        Vector3 newPosition = transform.position + movement * paddleMoveSpeed * Time.fixedDeltaTime;
        rb.AddForce(movement * paddleMoveSpeed, ForceMode2D.Force);
        newPosition.x = Mathf.Clamp(newPosition.x, -40f, 40f);
        newPosition.y = Mathf.Clamp(newPosition.y, -35f, 35f);
        if((Mathf.Abs(newPosition.x) < 2.1) && (-3.8 < newPosition.y && newPosition.y < 2.0)) {
            if(rb != null) rb.MovePosition(newPosition);
        }
    }
    void UpdateForMobile()
    {
        // Handle touch input
        Vector3 targetPosition = new Vector3(0,0,-10000);
        bool isMoving = true;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch info

            // If the touch just began or is moving
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                // Calculate the distance from the camera to the object to maintain its Z-position
                float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
                Vector3 touchScreenPosition = new Vector3(touch.position.x, touch.position.y, distanceFromCamera);

                // Convert screen touch position to world coordinates
                targetPosition = Camera.main.ScreenToWorldPoint(touchScreenPosition);

                // Preserve the original Z-coordinate of the object to prevent unwanted Z-axis movement
                targetPosition.z = transform.position.z;

                isMoving = true; // Start moving towards the new target
            }
        }
        // If the object is set to move
        if (isMoving)
        {
            // Smoothly move the object from its current position to the target position using Lerp
            var errVec = new Vector3(0,0,-10000);
            if(targetPosition != errVec) { 
                transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);

                // If the object is very close to the target position, stop moving
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    isMoving = false;
                }
            }
        }
    }
}
