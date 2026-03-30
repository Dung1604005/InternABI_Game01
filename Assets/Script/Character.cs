using UnityEngine;

/// <summary>
/// This class is the parent class of player end enemy
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnimName;
    private float hp;

    public bool IsDead => hp <=0;

    void Awake()
    {
        OnInit();
    }

    /// <summary>
    /// this function init all basic state or stat of character   
    /// </summary>
    protected virtual void OnInit()
    {
        hp = 100f;
    }


    /// <summary>
    /// Called immediately before character is disabled
    /// 
    /// </summary>
    protected virtual void OnDeSpawn()
    {
        
    }

    /// <summary>
    /// This function is called when character die
    /// Handle deadth sequence, starting with death animation
    /// </summary>
    protected virtual void OnDeath()
    {
        
        // play animation death
        ChangeAnim("die");
        Invoke(nameof(OnDeSpawn), 2f);
    }
    /// <summary>
    /// <see langword="this"/> function change the Animator of character <see langword="by"/> <paramref name="animName"/> trigger 
    /// </summary>
    /// <param name="animName"></param>

    protected void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            // Clear pending triggers and Set new trigger anim
            animator.ResetTrigger(animName);
            if(currentAnimName != null)
            {
                animator.ResetTrigger(currentAnimName);
            }
            currentAnimName = animName;

            animator.SetTrigger(animName);
        }
    }

    /// <summary>
    /// this function is call when character is attacked.
    /// Then calculate the remaining hp of character   
    /// </summary>
    /// <param name="damage"></param>

    public void OnHit(float damage)
    {
        
        // if character is not dead then calculate the remaining hp
        if (!IsDead)
        {
            hp = Mathf.Max(0f, hp - damage);
            Debug.Log("HIT " + gameObject.tag + " " + hp);
            // Check if character is death ?
            if(hp <= 0.1f)
            {
                OnDeath();
            }
        }
    }

    
    
}
