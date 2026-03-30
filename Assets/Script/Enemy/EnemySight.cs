using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Player")
        {
            enemy.SetTarget(collider2D.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Player")
        {
            enemy.SetTarget(null);
        }
    }
}
