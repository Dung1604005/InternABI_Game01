using UnityEngine;


// This class is like a bridge to connect playerController and animation
// => Can use animation event with function in PlayerController
public class AnimationEventHandler : MonoBehaviour
{
    private PlayerController playerController;


    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
    

    public void OnEndAttack()
    {
        
    }

    
}
