using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // If attack Area trigger with a game object with component Character 
        // => Cause damage for that character
        if(collider2D.tag == "Enemy" || collider2D.tag == "Player")
        {
            
            collider2D.GetComponent<Character>().OnHit(30f);
        }
    }
}
