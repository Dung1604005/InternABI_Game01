using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeCard> upgradeCardList;

    [SerializeField] private List<CardUpgrade> cardUpgradeList;

    [ContextMenu("Random new card")]
    public void OnInit()
    {
        foreach(UpgradeCard upgradeCard in upgradeCardList)
        {
            upgradeCard.ActiveCard();
        }
        // the purpose of this list is storing removed Card to give it back to original list 
        // after Init for all UpgradeCard
        List<CardUpgrade> removedCard = new List<CardUpgrade>();
        foreach(UpgradeCard upgradeCard in upgradeCardList)
        {
            // Random 1 card from card upgrade and dont let it be duplicated
            int randomValue = Random.Range(0, cardUpgradeList.Count);

            upgradeCard.OnInit(cardUpgradeList[randomValue]);

            removedCard.Add(cardUpgradeList[randomValue]);

            cardUpgradeList.RemoveAt(randomValue);
            
        }
        // Give all the removed card back
        foreach(CardUpgrade cardUpgrade in removedCard)
        {
            cardUpgradeList.Add(cardUpgrade);
        }
        // clear
        removedCard = null;

        
    }

    public void OnSelectedCard(UpgradeCard selectedCard)
    {
        // When one card is selected , all other card is deActive
        foreach(UpgradeCard upgradeCard in upgradeCardList)
        {
            if(upgradeCard != selectedCard)
            {
                upgradeCard.DeActiveCard();
            }
            
        }
    }

    void Start()
    {
        OnInit();
    }
}
