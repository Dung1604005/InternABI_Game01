using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : Character
{
    [Header("CheckGround Condition")]

    [SerializeField] private LayerMask layerGround;

    [SerializeField] private float distanceCheckGround;

    [Header("Reference")]

    

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private CombatSystem combatSystem;

    public CombatSystem CombatSystem => combatSystem;


    [Header("Stat Player")]
    //Store current shield player have

    [SerializeField] private int currentShields;

    [SerializeField] private float jumpForce;

    [SerializeField] private PlayerStat playerStat;

    public PlayerStat PlayerStat => playerStat;

    private bool isGround;

    private bool isJumping;

    // a flag to check whether if player have input to jump or not

    private bool jumpRequested = false;

   
    private int coin = 0;

    private float horizontalInput;

    

    private Vector3 savePoint;

    protected override void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
        OnInit();
    }

    

    protected override void OnInit()
    {
        

        //Set all state to origin
        
        
        
        isJumping = false;
        jumpRequested = false;
        currentHp = playerStat.MaxHealth;
        currentShields = playerStat.Shields;
        currentDamage = playerStat.Damage;
        healthBar.OnInit(currentHp, this.transform);
        // Update UI shield to display
        UIManager.Instance.SetShield(currentShields);
        

        //Reset player position to save point position
        transform.position = savePoint;

        ChangeAnim("idle");
        // clear all attack state, stat...
        combatSystem.OnInit();
        SavePoint();

        UIManager.Instance.SetCoin(coin);

    }

    protected override void OnDeSpawn()
    {
        // Respawn after death
        base.OnDeSpawn();
        playerStat.OnDeSpawn();
        OnInit();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// This function store current position of player <see langword="into"/> savePoint 
    /// </summary>
    public void SavePoint()
    {
        savePoint = transform.position;
    }

     /// <summary>
    /// 
    /// </summary>
    /// <param name="healAmount"></param>

    public void OnHeal(float healAmount)
    {
        // if character is not dead then update health
        if (!IsDead)
        {
            // Dont let health of character over maxHp
            currentHp = Mathf.Min(playerStat.MaxHealth, currentHp + healAmount);
            //Update current hp for healthbarUI
            healthBar.SetNewHp(currentHp);
        
        }
    }

    public override void OnHit(float damage)
    {
        // If player have shield, use shield to prevent damage;
        if(currentShields > 0)
        {
            // Update UI shield to display
            
            currentShields -= 1;
            UIManager.Instance.SetShield(currentShields);
            return;// Stop to not get hit
        }
        base.OnHit(damage);
    }
    public void UpdateDamage(float newDamage)
    {
        currentDamage = newDamage;
    }
    public void RestoreShields()
    {
        // Update UI shield to display
        
        currentShields = playerStat.Shields;
        UIManager.Instance.SetShield(currentShields);
    }

    
    void Start()
    {
        coin = PlayerPrefs.GetInt("coin", 0);

    }

    //Handle physics logic inside FixedUpdate to sync with Unity's physics system
    void FixedUpdate()
    {
        // Player died => Stop update anything
        if (base.IsDead)
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
        if (base.IsDead)
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
            if (combatSystem.CanAttackMelee())
            {
                rb.linearVelocity = Vector2.zero;
                combatSystem.ExecuteAttackMelee();
            }
            
        }

        //Throw
        if(Input.GetKeyDown(KeyCode.C) && isGround)
        {
            if (combatSystem.CanAttackKunai())
            {
                rb.linearVelocity = Vector2.zero;
                combatSystem.ExecuteThrowKunai();
            }
        }

        

        
    }
    /// <summary>
    /// Handle the logic Jump and fall
    /// </summary>

    private void OnJump()
    {
        // Stop moving while jump
        if (combatSystem.IsAttacking)
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
        if (combatSystem.IsAttacking)
        {
            return;
        }
        
        // Float values aren't perfectly precise. We use > 0.1f to ignore float inaccuracies.
        if(Mathf.Abs(horizontalInput) > 0.1f)
        {
            //Stop anim run interupt jumping animation
            if (!isJumping )
            {
                ChangeAnim("run");
            }
            
            //Make the player move by give player value of velocity
            rb.linearVelocity = new Vector2(horizontalInput*playerStat.SpeedMove, rb.linearVelocity.y);

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

    
    
    

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collide with a coin, destroy coin and increase the amount coint of player
        if(collision.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            Destroy(collision.gameObject);
            UIManager.Instance.SetCoin(coin);
        }

        //The player collide with Deadzone => player die
        if(collision.tag == "DeadZone")
        {
            OnHit(99999f);
            ChangeAnim("die");

            //Init the player stat, state after 1s
            Invoke(nameof(OnInit), 1f);

        }
    }
}

