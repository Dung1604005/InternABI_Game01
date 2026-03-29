using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;

    [SerializeField] private float speed;

    private Vector3 target;

    void Awake()
    {
        //Set first direction is moving from A to B
        transform.position = aPoint.position;

        target = bPoint.position;
    }


    void FixedUpdate()
    {
        //Update position of platform every frame without physic logic

        transform.position  = Vector3.MoveTowards(transform.position, target, Time.deltaTime*speed);

        //Check if the gameObject is reach target aPoint
        // Then switch the target to bPoint 
        // And vice versa
        if((transform.position - aPoint.position).sqrMagnitude <= 0.1f)
        {
            target = bPoint.position;
        }
        else if((transform.position - bPoint.position).sqrMagnitude <= 0.1f)
        {
            target = aPoint.position;
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collider with moving platform
        //=> Set moving platform is the parent of player
        // => this make player is move with the moving platform
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the player exit collider with moving platform
        //=> player is no longer need to move with the moving platform
        
        if(collision.transform.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
