using UnityEngine;

public class IdleState: IState
{

    float timer;

    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0f;
        randomTime = Random.Range(2.5f,4f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
       
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
