using UnityEngine;

public class CharacterController2025 : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 20f;
    public float jumpBoostPower = 7f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float gravityMultiplier = 2f;

    private bool isGrounded;
    private float coyoteTimer;
    private float jumpBufferTimer;
    private bool canJump = true;
    private Rigidbody rb;
    private Animator animator;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);

        float horizontalSpeed = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector3(horizontalSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        isGrounded = IsGrounded();

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            rb.linearVelocity += Vector3.down * gravityMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0 && coyoteTimer > 0 && canJump)
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
            jumpBufferTimer = 0;
            coyoteTimer = 0;
            canJump = false;
            Invoke(nameof(ResetJump), 0.1f);
        }

        if (Input.GetKey(KeyCode.Space) && !isGrounded && rb.linearVelocity.y > 0)
        {
            rb.AddForce(Vector3.up * jumpBoostPower, ForceMode.Acceleration);
        }

        if (horizontalAmount == 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * (1f - Time.deltaTime * 4f), rb.linearVelocity.y,
                rb.linearVelocity.z);
        }

        if (horizontalAmount != 0f)
        {
            float yawDirection = (horizontalAmount > 0f) ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0f, yawDirection, 0f);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalAmount));
        animator.SetBool("IsGrounded", isGrounded);
    }

    private bool IsGrounded()
    {
        float castDistance = capsuleCollider.bounds.extents.y + 0.1f;
        Vector3 boxSize = new Vector3(capsuleCollider.radius, 0.1f, capsuleCollider.radius);
        return Physics.BoxCast(transform.position, boxSize / 2, Vector3.down, Quaternion.identity, castDistance);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}