using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    private float xMove;

    private Rigidbody2D rb;

    private bool jumpFlag = false;

    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
{
    Debug.Log("SPACE PRESSED");
}

Debug.Log("Grounded: " + IsGrounded());

if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpFlag = true;
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement (NO deltaTime here)
        rb.linearVelocity = new Vector2(xMove * movementSpeed, rb.linearVelocity.y);

        // Jump
        if (jumpFlag)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            jumpFlag = false;
        }
    }

    private bool IsGrounded()
    {
        float radius = GetComponent<Collider2D>().bounds.extents.x;
        float dist = GetComponent<Collider2D>().bounds.extents.y;

        return Physics2D.CircleCast(transform.position, radius, Vector2.down, dist + 0.1f, ground);

    }
}
