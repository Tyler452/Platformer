using UnityEngine;

public class CharacterController2025 : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 15f;
    public float jumpBoostPower = 5.7f;

    public bool isGrounded;
    private Rigidbody rb;

    public float cameraScrollSpeed = 5f; 
    private Camera mainCamera; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);

        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;

        Collider c = GetComponent<Collider>();
        float castDistance = c.bounds.extents.y + 0.01f;
        Vector3 startPoint = transform.position;

        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector3.down * castDistance, color, 0, false);
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostPower, ForceMode.VelocityChange);
            }
        }

        if (horizontalAmount == 0f)
        {
            Vector3 decayedVelocity = rb.linearVelocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayedVelocity;
        }
        else
        {
            float yawDirection = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawDirection, 0f);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            mainCamera.transform.Translate(Vector3.right * cameraScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mainCamera.transform.Translate(Vector3.left * cameraScrollSpeed * Time.deltaTime);
        }
    }
}