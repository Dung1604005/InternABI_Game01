using UnityEngine;

[CreateAssetMenu(fileName = "CardHeal", menuName = "Scriptable Objects/CardHeal")]
public class CardHeal : CardUpgrade
{
   

    //Heal scale on max health percent

    [SerializeField] private float healAmountPercent;

    public override void ApplyCard(PlayerController playerController)
    {
        // Heal for player
        playerController.OnHeal(playerController.PlayerStat.MaxHealth*healAmountPercent);
    }
}
