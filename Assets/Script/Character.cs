using UnityEngine;

/// <summary>
/// This class is the parent class of player end enemy
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] protected HealthBar healthBar;

    [SerializeField] protected CombatText combatTextPrefab;
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
        healthBar.OnInit(100, this.transform);
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
        if(IsDead && animName != "die")
        {
            return;
        }
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
            //Update current hp for healthbarUI
            healthBar.SetNewHp(hp);
            // Check if character is death ?
            if(hp <= 0.1f)
            {
                OnDeath();
            }

            // Spawn a text display hp lost on top of character
            CombatText combatText = Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            combatText.OnInit(damage);
        }
    }

    
    
}
