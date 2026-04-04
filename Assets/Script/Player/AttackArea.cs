using System.Collections;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private bool isFreezing = false;
    private float damage;

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // If attack Area trigger with a game object with component Character 
        // => Cause damage for that character
        if(collider2D.tag == "Enemy" || collider2D.tag == "Player")
        {
            
            collider2D.GetComponent<Character>().OnHit(30f);
            TriggerHitStop();
        }
    }
    

    //This function is trigger when attack player or enemy
    public void TriggerHitStop(float duration = 0.05f) 
    {
        if (isFreezing) return; // If it is freezed then dont interupt it
        StartCoroutine(DoHitStop(duration));
    }

    private IEnumerator DoHitStop(float duration)
    {
        isFreezing = true;
        
        //Move every thing in game freezed
        Time.timeScale = 0f; 

        // wait a time in real Life
        yield return new WaitForSecondsRealtime(duration); 

        // Bring time back normal
        Time.timeScale = 1f; 
        
        isFreezing = false;
    }
}
