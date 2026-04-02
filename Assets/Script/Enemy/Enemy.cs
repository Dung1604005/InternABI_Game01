using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;

    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject attackAreaGameobject;

    private IState currentState;

    private Character target;

    public Character Target => target;

    private bool isRight = true;
    protected override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();
    }

    protected override void OnDeSpawn()
    {
        
        base.OnDeSpawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        rb.linearVelocity = Vector2.zero;
        base.OnDeath();
    }

    public void Attack()
    {
        ChangeAnim("attack");

        // Turn on the attack area then turn off it after 0.5s
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Moving()
    {
        //make enemy run in its facing direction
        ChangeAnim("run");
        rb.linearVelocity = transform.right*moveSpeed;

    }

    public void StopMoving()
    {
        //Change state to idle and set current velocity to zero to stop
        ChangeAnim("idle");
        rb.linearVelocity = Vector2.zero;
    }
    public void ChangeState(IState newState)
    {
        //Exit current state cleanup.
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        //Enter the new state
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void ChangeDirection(bool isRight)
    {
        // Set the value for parameter isRight of enemy
        this.isRight = isRight;

        //If isRight is true then dont rotate anything because enemy is face right at the beginning
        // Else rotate Y 180 degree make enemy face left
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) :
        Quaternion.Euler(Vector3.up*180);
    }

    
    public bool IsTargetInRange()
    {
        // Check if the player target is in range Attack or not ?
        
        return (target!= null) &&(target.transform.position - this.transform.position).sqrMagnitude <= attackRange*attackRange;
    }
    public void SetTarget(Character character)
    {
        //Set the target
        this.target = character;

        //If the player in range Attack => Change to state Attack 
        if(target != null && IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        // If the player is not in range Attack => Make enemy chase player
        else if(target != null)
        {
            ChangeState(new PatrolState());
        }
        // If not found player => Change enemy to Idle state
        else
        {
            ChangeState(new IdleState());
        }
    }
    void Update()
    {
        // Execute for currentState
        if(currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When enemy enter colider with enemyWall, reverse the direction of enemy
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    private void ActiveAttack()
    {
        attackAreaGameobject.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackAreaGameobject.SetActive(false);
    }

    
}
