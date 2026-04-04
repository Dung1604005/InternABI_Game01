using UnityEngine;
[CreateAssetMenu(fileName = "CardUpgradeKunai", menuName = "Scriptable Objects/CardUpgradeKunai")]
public class CardUpgradeKunai : CardUpgrade
{

    public override void ApplyCard(PlayerController playerController)
    {
        // increase level kunai
        playerController.CombatSystem.LevelUpKunaiAttack();
    }
}
