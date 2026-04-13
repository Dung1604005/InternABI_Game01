using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private CardManager cardManager;
    private Animator anim;
    private Button cardButton;

    [SerializeField] private CardUpgrade cardUpgrade;

    [SerializeField] private Image border;

    [SerializeField] private Sprite epicBorderSprite;
    [SerializeField] private Sprite rareBorderSprite;

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI descriptionText;

     public void OnInit(CardUpgrade cardUpgrade)
    {
        // Change border
        if(cardUpgrade.Rarity == Rarity.RARE)
        {
            border.sprite = rareBorderSprite;
        }
        else
        {
            border.sprite = epicBorderSprite;
        }
        // Set information for card
        this.cardUpgrade = cardUpgrade;
        nameText.text = cardUpgrade.CardName;

        descriptionText.text = cardUpgrade.Description;
        
    }
    // This function is called when player click

    public void OnCardSelected()
    {
        // Dont let player click twice or move
        cardButton.interactable = false;
        
        // De active this child
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        cardManager.OnSelectedCard(this);

        // Play anim destroy card
        anim.SetTrigger("Destroy");

        //Apply upgrade card on player

        cardUpgrade.ApplyCard(playerController);
    }

    public void DeActiveCard()
    {
        this.gameObject.SetActive(false);
        

    }

    public void ActiveCard()
    {
       
        
        // Active this object and its child
        this.gameObject.SetActive(true);
        cardButton.interactable = true;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cardButton = GetComponent<Button>();
        cardManager = GetComponentInParent<CardManager>();
        
    }
   
}
