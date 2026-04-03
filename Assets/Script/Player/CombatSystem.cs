using System;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private bool isAttacking;

    public bool IsAttacking => isAttacking;

    [Header("Attack Melee")]

    // damage of this attack = damageScale * atk of player
    [SerializeField] private float damageScaleMelee;

    [SerializeField] private bool canCombo;

    
    // player have to unlock combo with card Upgrade
    [SerializeField] private bool unlockedCombo;

    private float timerAttackMelee;

    [SerializeField] private float cooldownAttackMelee;

    [SerializeField] private GameObject attackAreaGameobject;

    [Header("Throw Kunai")]

    [SerializeField] private float damageScaleKunai;

    private float timerAttackKunai;

    [SerializeField] private float cooldownAttackKunai;

    [SerializeField] private Kunai kunaiPrefab;

    [SerializeField] private Transform throwPoint;

    // This equal number of kunai throwed when player throw 
    [SerializeField] private int levelModifyKunai;


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

    /// <summary>
    /// This function check if player can attack melee ?
    /// (CDR is applied on this) 
    /// </summary>
    /// <returns></returns>
   
    public bool CanAttackMelee()
    {
        return timerAttackMelee >= cooldownAttackMelee*(1f- playerController.PlayerStat.Cdr);
    }

    /// <summary>
    /// This function check if player can <see langword="throw"/> kunai  ?
    /// (CDR is applied on this) 
    /// </summary>
    /// <returns></returns>

    public bool CanAttackKunai()
    {
        return timerAttackKunai >= cooldownAttackKunai*(1f- playerController.PlayerStat.Cdr);
    }

    public void ExecuteAttackMelee()
    {
        // If player haven't attacked yet
        if (!isAttacking)
        {
            // Reset timer
            timerAttackMelee = 0f;
            attackAreaGameobject.SetActive(true);
            isAttacking = true;
            // Activate Anim attack
            playerController.SetCurrentAnim("attack");
            playerController.Anim.SetTrigger("attack"); 
        }
        // If player is attacking and can combo
        else if (canCombo && unlockedCombo)
        {
            playerController.Anim.SetTrigger("attack"); 
            canCombo = false; // Wait for open window combo
        }
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
    }

    /// <summary>
    /// This was designed to attach to Animation event to end combo attack
    /// </summary>
    public void FinishCombo()
    {
        // Stop attacking and prevent combo and turn off attack Area
        attackAreaGameobject.SetActive(false);
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
        playerController.ChangeAnim("throw");
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    /// <summary>
    /// Increase length of combo
    /// </summary>
    /// <param name="bonusComboLength"></param>
    public void UnlockComboMelee()
    {
        unlockedCombo  = true;
    }

    public void LevelUpKunaiAttack()
    {
        levelModifyKunai = Math.Max(3, 1 + levelModifyKunai);
    }

    public void OnInit()
    {
        // Reset all timer to 0f and modify of kunai , combo lock
        timerAttackKunai = 0f;
        timerAttackMelee = 0f;
        levelModifyKunai = 1;
        isAttacking =false;
        canCombo = false;
        unlockedCombo = false;
        attackAreaGameobject.SetActive(false);

    }













}
