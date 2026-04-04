using UnityEngine;

[CreateAssetMenu(fileName = "CardUnlockAttackCombo", menuName = "Scriptable Objects/CardUnlockAttackCombo")]
public class CardUnlockAttackCombo : CardUpgrade
{
    

    public override void ApplyCard(PlayerController playerController)
    {
        // Unlock combo melee
        playerController.CombatSystem.UnlockComboMelee();
    }
}
