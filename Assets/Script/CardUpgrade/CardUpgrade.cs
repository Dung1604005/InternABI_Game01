using UnityEngine;

public abstract class CardUpgrade: ScriptableObject
{
     [SerializeField] private string idCard;

    public string IdCard => idCard;
    [SerializeField] private string cardName;

    public string CardName => cardName;

    [TextArea(3, 10)]

    [SerializeField] private string description;

    public string Description => description;

    [SerializeField] private Rarity rarity;

    public Rarity Rarity => rarity;

    [SerializeField] private int stackCount;

    public int StackCount => stackCount;

    /// <summary>
    /// This function apply upgrade of card into player
    /// </summary>
    /// <param name="player"></param>
    public virtual void ApplyCard(PlayerController player)
    {
        
    }
}

public enum Rarity
{
    RARE,
    EPIC
}
