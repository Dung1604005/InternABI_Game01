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
        if(timer > randomTime)
        {
            enemy.ChangeState(new PatrolState());
        }
        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
