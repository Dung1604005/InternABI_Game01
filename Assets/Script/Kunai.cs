using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    void Start()
    {
        OnInit();
        
    
        
    }

    public void OnInit()
    {
        // Make kunai fly in its facing direction
        rb.linearVelocity = transform.right*5;
        // Destroy kunai after 3s
        Invoke(nameof(OnDespawn), 3f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        // If kunai collider with enemy
        // Then cause damage for enemy and destroy kunai
        if(collider2D.tag == "Enemy")
        {
            collider2D.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
