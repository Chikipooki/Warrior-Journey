using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpWallPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public Joystick joystick;


    private void Awake()
    {
        //Referenses for rb and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        //horizontalInput = joystick.Horizontal;
        horizontalInput = Input.GetAxis("Horizontal");


        // Flip player when mooving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(5, 5, 5);
        if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-5, 5, 5);

        // Set animator parametres
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("wallSlide", onWall());

        // Wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 5;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    //public void OnJumpButtonDown()
    //{
    //    if (isGrounded())
    //    {
    //        body.velocity = new Vector2(body.velocity.x, jumpPower);
    //        anim.SetTrigger("jump");
    //    }
    //}

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        if (onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpWallPower, 15);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * 5, transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpWallPower, 15);

            wallJumpCooldown = -1;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CircleCast(circleCollider.bounds.center, circleCollider.radius, Vector2.down, 0.4f, groundLayer);
        return raycastHit.collider != null;
    }    
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
