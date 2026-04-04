using UnityEngine;

[CreateAssetMenu(fileName = "CardModifyStat", menuName = "Scriptable Objects/CardModifyStat")]
public class CardModifyStat : CardUpgrade
{
   

    // Type of player stat and increase amount that this card give

    [SerializeField] private PlayerStatType playerStatType;

    [SerializeField] private float increaseAmount;

    public override void ApplyCard(PlayerController playerController)
    {
        // Apply modify stat player
        playerController.PlayerStat.ApplyUpgradeStat(playerStatType, increaseAmount);
    }

    
}
