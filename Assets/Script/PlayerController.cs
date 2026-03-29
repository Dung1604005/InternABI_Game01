using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : Character
{
    [Header("CheckGround Condition")]

    [SerializeField] private LayerMask layerGround;

    [SerializeField] private float distanceCheckGround;

    [Header("Reference")]

    

    [SerializeField] private Rigidbody2D rb;

    [Header("Stat Player")]

    [SerializeField] private float speedMove;

    [SerializeField] private float jumpForce;



    private bool isGround;

    private bool isJumping;

    // a flag to check whether if player have input to jump or not

    private bool jumpRequested = false;

    private bool isAttack;

    private int coin = 0;

    private bool isDeath = false;

    private float horizontalInput;

    

    private Vector3 savePoint;

    

    protected override void OnInit()
    {
        //CALL BASE METHOD: Ensure base class cleanup logic is executed.
        base.OnInit();

        //Set all state to origin
        isDeath = false;
        isAttack = false;
        isJumping = false;
        jumpRequested = false;

        //Reset player position to save point position
        transform.position = savePoint;

        ChangeAnim("idle");

    }

    protected override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }

    /// <summary>
    /// This function store current position of player <see langword="into"/> savePoint 
    /// </summary>
    public void SavePoint()
    {
        savePoint = transform.position;
    }

    void Awake()
    {
        SavePoint();
        
    }

    
    void Start()
    {

    }

    //Handle physics logic inside FixedUpdate to sync with Unity's physics system
    void FixedUpdate()
    {
        // Player died => Stop update anything
        if (isDeath)
        {
            return;
        }
        //Check if whether the player is on the ground
        isGround = CheckGrounded();

        //Move
        OnMove();
        //Jump
        OnJump();
        

        

    }

    void Update()
    {
        // Player died => Stop update anything
        if (isDeath)
        {
            return;
        }
        // value is in range -1 to 1
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // If player click Space and the player is on the ground
        // => Send signal for FixedUpdate handle logic jump
        if(Input.GetKeyDown(KeyCode.Space) && isGround && !isJumping)
        {
            jumpRequested = true;
        }
        
        // Attack
        if(Input.GetKeyDown(KeyCode.Mouse0) && isGround)
        {
            Attack();
        }

        //Throw
        if(Input.GetKeyDown(KeyCode.C) && isGround)
        {
            Throw();
        }

        

        
    }
    /// <summary>
    /// Handle the logic Jump and fall
    /// </summary>

    private void OnJump()
    {
        // Stop moving while jump
        if (isAttack)
        {
            return;
        }
        
        
        // Check the condition if the player is in the Air and have velocity.y negative
        // => Set Player fall
        if(!isGround && rb.linearVelocity.y < -0.1f )
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        // Apply jump force only if a jump was requested and the player is on the ground
        if (jumpRequested && isGround)
        {
            ChangeAnim("jump");
            rb.AddForce(jumpForce*Vector2.up, ForceMode2D.Impulse);
            isJumping = true;

            // Stop jump signal so that next frame player dont jump
            jumpRequested = false;
        }
    }

    /// <summary>
    /// This function make the player move with horizontalInput
    /// </summary>

    private void OnMove()
    {
        // Stop moving while attacking
        if (isAttack)
        {
            return;
        }
        
        // Float values aren't perfectly precise. We use > 0.1f to ignore float inaccuracies.
        if(Mathf.Abs(horizontalInput) > 0.1f)
        {
            //Stop anim run interupt jumping animation
            if (!isJumping)
            {
                ChangeAnim("run");
            }
            
            //Make the player move by give player value of velocity
            rb.linearVelocity = new Vector2(horizontalInput*Time.fixedDeltaTime*speedMove, rb.linearVelocity.y);

            // Flip character based on input
            // If input < 0.1f then flip character
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontalInput < -0.1f ? 180:0, 0));
        }
        else if(isGround)
        {
            //Stop anim idle interupt jumping animation
        
            if (!isJumping)
            {
                ChangeAnim("idle");
            }
            
            //Make the player dont move instantly
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// This function check <see langword="if"/>  player <see langword="is"/> stay <see langword="on"/> the ground by Raycast
    /// </summary>
    private bool CheckGrounded()
    {
        Debug.DrawLine(this.transform.position, (Vector2)transform.position + Vector2.down*distanceCheckGround, Color.red);
        // This logic shoot a laser from the player and with direction down in distance distanceCheckGround
        // Only object with ground layer can be shooted by this laser
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, distanceCheckGround, layerGround);

        // If laser find a ground it will return a collider => player is on the ground
        return hit.collider != null && rb.linearVelocity.y <= 0.01f;
    }

    /// <summary>
    /// Handle the logic attack
    /// </summary>
    private void Attack()
    {
        // Stop attack while attacking
        if (isAttack)
        {
            return;
        }
        rb.linearVelocity  = Vector2.zero;
        isAttack = true;
        ChangeAnim("attack");

        // Goi ham ResetAttack sau 0.5f
        Invoke(nameof(ResetAttack), 0.5f);
    }
    /// <summary>
    /// Reset Attack of player
    /// </summary>

    private void ResetAttack()
    {
        // Check if player is in attackState ?
        if (isAttack)
        {
            ChangeAnim("idle");
            isAttack = false;
        }
    }
    /// <summary>
    /// Handle the logic throw knife
    /// </summary>
    private void Throw()
    {

        // Stop throw while attacking
        if (isAttack)
        {
            return;
        }
        rb.linearVelocity  = Vector2.zero;
        isAttack = true;
        ChangeAnim("throw");
        // Goi ham ResetAttack sau 0.5f
        Invoke(nameof(ResetAttack), 0.5f);
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collide with a coin, destroy coin and increase the amount coint of player
        if(collision.tag == "Coin")
        {
            coin++;
            Destroy(collision.gameObject);
        }

        //The player collide with Deadzone => player die
        if(collision.tag == "DeadZone")
        {
            isDeath = true;
            ChangeAnim("die");

            //Init the player stat, state after 1s
            Invoke(nameof(OnInit), 1f);

        }
    }
}

