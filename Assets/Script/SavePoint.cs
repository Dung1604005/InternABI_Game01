using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Collision with player => check new savePoint for player
        if(collider2D.tag == "Player")
        {
            collider2D.GetComponent<PlayerController>().SavePoint();
        }
    }
}
