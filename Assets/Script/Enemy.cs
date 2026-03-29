using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;

    [SerializeField] private float moveSpeed;

    private IState currentState;
    protected override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    protected override void OnDeSpawn()
    {
        base.OnDeSpawn();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }

    public void Attack()
    {
        
    }

    public void Moving()
    {
        

    }

    public void StopMoving()
    {
        
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
    void Update()
    {
        // Execute for currentState
        if(currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
}
