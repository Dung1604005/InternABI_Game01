using UnityEngine;

public interface IState 
{
    /// <summary>
    /// This function run when enemy Enter the state
    /// </summary>
    /// <param name="enemy"></param>
    void OnEnter(Enemy enemy);

    /// <summary>
    /// This function run when enemy update while in this state
    /// </summary>
    /// <param name="enemy"></param>
    void OnExecute(Enemy enemy);

    /// <summary>
    /// This function run when enemy exit this state
    /// </summary>
    /// <param name="enemy"></param>
    void OnExit(Enemy enemy);
}
