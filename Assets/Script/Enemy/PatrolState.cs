using UnityEngine;

public class PatrolState : IState
{
    float timer;

    float randomTime;
    public void OnEnter(Enemy enemy)
    {
        timer = 0f;
        randomTime = Random.Range(3, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            //Change direction of enemy to face the target
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
            
        }
        else
        {
            if (timer >= randomTime)
            {
                enemy.ChangeState(new IdleState());
            }
            else
            {
                enemy.Moving();
            }
        }
       

    }

    public void OnExit(Enemy enemy)
    {

    }
}
