using UnityEngine;

public class PatrolState: IState
{
    float timer;

    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        randomTime = Random.Range(3,6f);
    }

    public void OnExecute(Enemy enemy)
    {
        
        if(timer > randomTime)
        {
            enemy.ChangeState(new IdleState());
        }
        else
        {
            enemy.Moving();
        }
        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
