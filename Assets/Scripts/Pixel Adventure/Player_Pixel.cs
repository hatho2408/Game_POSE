using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Pixel : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] public bool testingOnPc;

    [Header("Particles")]
    [SerializeField] private ParticleSystem dustFx;
    private float dustFxTimer;


    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;

    private float defaultJumpForce;

    private bool canDoubleJump = true;
    private bool canMove;

    private bool canBeControlled;

    private bool readyToLand;


    [SerializeField] private float bufferJumpTime;
    private float bufferJumpCounter;

    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpCounter;
    private bool canHaveCayoteJump;

    private float defaultGravityScale;
    [Header("Knockback info")]
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackProtectionTime;

    private bool isKnocked;
    private bool canBeKnocked = true;
    [Header("Collision info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    private RaycastHit2D isGrounded;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;


    private bool facingRight = true;
    private int facingDirection = 1;

    [Header("Controlls info")]
    public VariableJoystick joystick;
    private float movingInput;
    private float vInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        SetAnimationLayer();

        defaultJumpForce = jumpForce;
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }


    void Update()
    {
        AnimationControllers();

        if (isKnocked)
            return;

        FlipController();
        CollisionChecks();
        InputChecks();
        CheckForEnemy();


        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {
            if (!canDoubleJump)
            {
                canDoubleJump = true;
            }

            canMove = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }

            canHaveCayoteJump = true;

            if (readyToLand)
            {
                dustFx.Play();
                readyToLand = false;
            }
        }
        else
        {
            if (!readyToLand)
                readyToLand = true;

            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }
        }


        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }


        Move();

    }

    private void CheckForEnemy()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy_Pixel>() != null)
            {

                Enemy_Pixel newEnemy = enemy.GetComponent<Enemy_Pixel>();

                if (newEnemy.invincible)
                    return;

                if (rb.velocity.y < 0)
                {
                    AudioManager_Pixel.instance.PlaySFX(1);

                    newEnemy.Damage();
                    anim.SetBool("flipping", true);
                    Jump();
                }
            }
        }
    }

    private void StopFlippingAnimation()
    {
        anim.SetBool("flipping", false);
    }

    private void SetAnimationLayer()
    {
        int skinIndex = PlayerManager_Pixel.instance.choosenSkinId;

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(skinIndex, 1);
    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetBool("canBeControlled", canBeControlled);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void InputChecks()
    {
        if (!canBeControlled)
            return;

        if (testingOnPc)
        {
            movingInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");
        }
        else 
        {
            movingInput = joystick.Horizontal;
            vInput = joystick.Vertical;
        }




        if (vInput < 0)
            canWallSlide = false;

        if (Input.GetButtonDown("Jump"))
            JumpButton();
    }

    public void ReturnControll()
    {
        rb.gravityScale = defaultGravityScale;
        canBeControlled = true;
    }

    public void JumpButton()
    {
        if (!isGrounded)
            bufferJumpCounter = bufferJumpTime;

        if (isWallSliding)
        {
            WallJump();
            canDoubleJump = true;
        }
        else if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce = defaultJumpForce;
        }

        canWallSlide = false;
    }

    public void Knockback(Transform damageTransform)
    {
        AudioManager_Pixel.instance.PlaySFX(9);

        if (!canBeKnocked)
            return;

        if (GameManager_Pixel.instance.difficulty > 1)
            PlayerManager_Pixel.instance.OnTakingDamage();

        PlayerManager_Pixel.instance.ScreenShake(-facingDirection);
        isKnocked = true;
        canBeKnocked = false;


        #region Define horizontal direction for knockback
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;
        #endregion

        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);
        Invoke("AllowKnockback", knockbackProtectionTime);
    }

    private void CancelKnockback()
    {
        isKnocked = false;
    }

    private void AllowKnockback()
    {
        canBeKnocked = true;
    }

    private void Move()
    {
        if (canMove)
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void WallJump()
    {
        AudioManager_Pixel.instance.PlaySFX(3);

        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);

        dustFx.Play();
    }

    private void Jump()
    {
        AudioManager_Pixel.instance.PlaySFX(3);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        if (isGrounded)
            dustFx.Play();
    }

    public void Push(float pushForce)
    {

        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController()
    {
        dustFxTimer -= Time.deltaTime;

        if (facingRight && rb.velocity.x < 0)
            Flip();
        else if (!facingRight && rb.velocity.x > 0)
            Flip();

    }

    private void Flip()
    {

        if (dustFxTimer < 0)
        {
            dustFx.Play();
            dustFxTimer = .7f;
        }

        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);

        if (isWallDetected && rb.velocity.y < 0)
            canWallSlide = true;

        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }
}
