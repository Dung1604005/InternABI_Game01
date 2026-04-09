using UnityEngine;

public interface IEvent
{
    
}

public struct OnEnemyKilled : IEvent
{
    public int amount;
}

public struct OnLevelChanged: IEvent
{
    public int level;

    public int totalEnemy;
}
