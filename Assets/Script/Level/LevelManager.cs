using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]private int currentLevel;

    [SerializeField] private int totalEnemyInLevel;

    [SerializeField] private int enemyKilledInLevel;

    private EventBinding<OnEnemyKilled> enemyKilledBinding;

    public void OnEnable()
    {
        EventBus<OnEnemyKilled>.Register(enemyKilledBinding);
    }

    public void OnDisable()
    {
        EventBus<OnEnemyKilled>.DeRegister(enemyKilledBinding);
    }

    public void OnInit()
    {
        currentLevel = 1;
        totalEnemyInLevel = 5;
        enemyKilledInLevel = 0;
        EventBus<OnLevelChanged>.Raise(new OnLevelChanged
        {
           level = currentLevel,
           totalEnemy = totalEnemyInLevel 
        });
    }

    public void LevelUp()
    {
        currentLevel += 1;
        totalEnemyInLevel = 5;
        enemyKilledInLevel = 0;

        EventBus<OnLevelChanged>.Raise(new OnLevelChanged
        {
           level = currentLevel,
           totalEnemy = totalEnemyInLevel 
        });
    }
    public void UpdateEnemyKilled(OnEnemyKilled onEnemyKilled)
    {
        enemyKilledInLevel += onEnemyKilled.amount;

        if(enemyKilledInLevel >= totalEnemyInLevel)
        {
            LevelUp();
        }
    }

    private void Awake()
    {
        enemyKilledBinding = new EventBinding<OnEnemyKilled>(UpdateEnemyKilled);
    }


}
