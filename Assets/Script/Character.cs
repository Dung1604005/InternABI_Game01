using UnityEngine;

/// <summary>
/// This class is the parent class of player end enemy
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Animator Anim => animator;

    [SerializeField] protected HealthBar healthBar;

    public HealthBar HealthBar => healthBar;

    [SerializeField] protected CombatText combatTextPrefab;
    protected string currentAnimName;

    //Store current hp character have
    protected float currentHp;

    protected float maxHp;

    protected float currentDamage;

    public bool IsDead => currentHp <=0;

    protected virtual void Awake()
    {
        OnInit();
    }

    /// <summary>
    /// this function init all basic state or stat of character   
    /// </summary>
    protected virtual void OnInit()
    {
        maxHp = 100f;
        currentHp = maxHp;
        healthBar.OnInit(currentHp, this.transform);
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
    public void SetCurrentAnim(string _currentAnimName)
    {
        currentAnimName = _currentAnimName;
    }
    /// <summary>
    /// <see langword="this"/> function change the Animator of character <see langword="by"/> <paramref name="animName"/> trigger 
    /// </summary>
    /// <param name="animName"></param>

    public void ChangeAnim(string animName)
    {
        if(IsDead && animName != "die")
        {
            return;
        }
        if(currentAnimName != animName)
        {
            // Clear pending triggers and Set new trigger anim
            animator.ResetTrigger(animName);
            
            currentAnimName = animName;

            animator.SetTrigger(animName);
        }
    }



    /// <summary>
    /// this function is call when character is attacked.
    /// Then calculate the remaining hp of character   
    /// </summary>
    /// <param name="damage"></param>

    public virtual void OnHit(float damage)
    {
        
        // if character is not dead then calculate the remaining hp
        if (!IsDead)
        {
            currentHp = Mathf.Max(0f, currentHp - damage);
            //Update current hp for healthbarUI
            healthBar.SetNewHp(currentHp);
            // Check if character is death ?
            if(currentHp <= 0.1f)
            {
                OnDeath();
            }

            // Spawn a text display hp lost on top of character
            CombatText combatText = Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            combatText.OnInit(damage);
        }
    }

   

    
    
}
