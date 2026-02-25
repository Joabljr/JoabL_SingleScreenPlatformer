using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    private float xMove;
    private float xVelocity;

    private Rigidbody2D rb;

    private bool jumpFlag = false;

    public GameObject meleeAttack;

    private float facingDirection;

    private float attackOffset = 0.1f;

    public float meleeDuration = 0.25f;
    private float timeElapsedSinceMelee = 0;

    private bool meleeTriggered = false;

    public GameObject bulletPrefab;

    public Transform firePoint;

    public Transform groundCheck;

    public LayerMask groundLayer;

    public int extraJumps = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }

    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
{
    jumpFlag = true;
}
else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
{
    jumpFlag = true;
    extraJumps--;
}



        if (Input.GetKeyDown(KeyCode.K))
        {
            RangedAttack();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            RangedAttack();
        }

        if (xMove > 0)
        {
            facingDirection = 1;
        }
        else if (xMove < 0)
        {
            facingDirection = -1;
        }

        if (meleeTriggered)
        {
            if (timeElapsedSinceMelee < meleeDuration)
            {
                timeElapsedSinceMelee += Time.deltaTime;
            }
            else
            {
                meleeAttack.SetActive(false);
                timeElapsedSinceMelee = 0;
                meleeTriggered = false;
            }
        }

        Debug.Log(IsGrounded());
    }

    private void FixedUpdate()
    {
        xVelocity = xMove * movementSpeed * Time.deltaTime;
        rb.linearVelocity = new Vector3(xVelocity, rb.linearVelocity.y, 0);

        if (jumpFlag)
        {
            rb.linearVelocityY = jumpSpeed;
            jumpFlag = false;
        }
    }

    private bool IsGrounded()
    {
        float radius = 0.2f;      // small detection circle
        float extra = 0.05f;      // tiny buffer

        return Physics2D.OverlapCircle(groundCheck.position, radius + extra, groundLayer);
    }

    private void MeleeAttack()
    {
        meleeTriggered = true;
        meleeAttack.SetActive(true);
        meleeAttack.transform.localPosition = new Vector3(attackOffset * facingDirection, meleeAttack.transform.localPosition.y, 0);
    }

    private void RangedAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().direction = new Vector2(facingDirection, 0);
    }

    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PowerUp>() != null)
        {
            collision.GetComponent<PowerUp>().ApplyEffect();
        }
    }*/
}
