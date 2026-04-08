using System;
using System.Collections;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private bool isAttacking;

    [Header("Attack Melee")]

    // damage of this attack = damageScale * atk of player
    [SerializeField] private float damageScaleMelee;

    [SerializeField] private bool canCombo;
    
    // player have to unlock combo with card Upgrade
    [SerializeField] private bool unlockedCombo;


    [SerializeField] private float cooldownAttackMelee;

    [SerializeField] private AttackArea attackArea;

    [Header("Throw Kunai")]

    [SerializeField] private float damageScaleKunai;

    [SerializeField] private float cooldownAttackKunai;

    [SerializeField] private Kunai kunaiPrefab;

    [SerializeField] private Transform throwPoint;

    // This equal number of kunai throwed when player throw 
    [SerializeField] private int levelModifyKunai;

    [SerializeField] private float intervalThrow;

    private float timerAttackMelee;

    private float timerAttackKunai;

    // This para control if player can combo next attack
    private bool isCombo;

    #region GETTER

    public bool IsAttacking => isAttacking;

    #endregion

    /// <summary>
    /// This function check if player can attack melee ?
    /// (CDR is applied on this) 
    /// </summary>
    /// <returns></returns>
   
    public bool CanAttackMelee()
    {
        return timerAttackMelee >= cooldownAttackMelee*(1f- playerController.PlayerStat.Cdr)|| canCombo;
    }

    /// <summary>
    /// This function check if player can <see langword="throw"/> kunai  ?
    /// (CDR is applied on this) 
    /// </summary>
    /// <returns></returns>

    public bool CanAttackKunai()
    {
        return timerAttackKunai >= cooldownAttackKunai*(1f- playerController.PlayerStat.Cdr) ;
    }

    public void ExecuteAttackMelee()
    {

        Debug.Log(isAttacking  + " " + canCombo );
        // If player haven't attacked yet
        if (!isAttacking)
        {
            // Reset timer
            timerAttackMelee = 0f;
            
            isAttacking = true;
            // Activate Anim attack
            playerController.SetCurrentAnim("attack");
            playerController.Anim.SetTrigger("attack"); 
        }
        // If player is attacking and can combo
        else if (canCombo && unlockedCombo)
        {
            Debug.Log("Start combo");
            playerController.SetCurrentAnim("combo");
            playerController.Anim.SetTrigger("combo"); 
            // Give a signal for code understand player can combo next attack
            isCombo = true;
            canCombo = false; // Wait for open window combo
        }
    }
    // Turn on attack area right on time player slash
    public void TriggerDamage()
    {
        attackArea.SetDamage(damageScaleMelee*playerController.PlayerStat.Damage);
        attackArea.gameObject.SetActive(true);
        
    }
    /// <summary>
    /// This was designed to attach to Animation event to open comboWindow
    /// </summary>

    public void OpenComboWindow()
    {
        // make player can combo
        canCombo = true;
        
    }

    /// <summary>
    /// This was designed to attach to Animation event to close comboWindow
    /// </summary>
    public void CloseComboWindow()
    {
        // prevent player combo
        canCombo = false;
        // Reset combo in last attack
        isCombo = false;

        attackArea.gameObject.SetActive(false);
    }

    /// <summary>
    /// This was designed to attach to Animation event to end combo attack
    /// </summary>
    public void FinishCombo()
    {
        // If player can combo next attack then dont finish attack
        if (isCombo)
        {
            return;
        }
        // Stop attacking and prevent combo and turn off attack Area
        attackArea.gameObject.SetActive(false);
        isAttacking = false;
        canCombo = false;
        
    }

    /// <summary>
    /// This was designed to end normal attack
    /// </summary>

    public void FinishAttack()
    {
        isAttacking = false;
    }

    /// <summary>
    /// Spawn kunai
    /// </summary>
    public void ExecuteThrowKunai()
    {
        isAttacking = true;
        // Reset timer
        timerAttackKunai = 0f;
        playerController.ChangeAnim("throw");

        //Start throw kunai
        StartCoroutine(SpawnKunai());
        
    }

    IEnumerator SpawnKunai()
    {
        // Throw number of kunai = levelModifyKunai
        for(int turn = 1; turn <= levelModifyKunai; turn++)
        {
            
            Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
            //wait for interval 
            yield return new WaitForSeconds(intervalThrow);
        }

        // Stop attack
        isAttacking = false;
    }

    

    /// <summary>
    /// unlock combat
    /// </summary>
    /// <param name="bonusComboLength"></param>
    public void UnlockComboMelee()
    {
        unlockedCombo  = true;
    }

    /// <summary>
    /// Increase the amount of number kunai can <see langword="throw"/> per 1 attack 
    /// </summary>

    public void LevelUpKunaiAttack()
    {
        levelModifyKunai = Math.Min(3, 1 + levelModifyKunai);
    }

    public void OnInit()
    {
        // Reset all timer to 0f and modify of kunai , combo lock
        timerAttackKunai = 0f;
        timerAttackMelee = 0f;
        levelModifyKunai = 1;
        isAttacking =false;
        canCombo = false;
        unlockedCombo = true;
        attackArea.gameObject.SetActive(false);

    }

    void Update()
    {
        if (isAttacking)
        {
            // Prevent attack when attacking
            return;
        }
        timerAttackMelee += Time.deltaTime;

        timerAttackKunai += Time.deltaTime;
    }













}
