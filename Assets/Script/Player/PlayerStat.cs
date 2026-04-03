using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private float damage;

    public float Damage => damage;

    [SerializeField] private float maxHealth;

    public float MaxHealth => maxHealth;

    [SerializeField] private int shields;

    public int Shields => shields;

    // cool down reduction
    [SerializeField] private float cdr;

    public float Cdr => cdr;

    // Max cool down reduction
    [SerializeField] private float maxCdr;

    public float MaxCdr => maxCdr;

    [SerializeField] private float speedMove;

    public float SpeedMove => speedMove;

    private PlayerController playerController;



    // Update new maxHealth
    public void SetMaxHealth(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    // Update new damage;
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    //Update new shield amount
    public void SetShields(int _shields)
    {
        shields =_shields;
    }

    //Update new Cdr
    public void SetNewCdr(float _cdr)
    {
        this.cdr = Mathf.Max(_cdr, maxCdr );
    }

    // classify stat type and upgrade stat
    public void ApplyUpgradeStat(PlayerStatType playerStatType, float amountIncrease)
    {
        switch (playerStatType)
        {
            case PlayerStatType.HEALTH:
               // Increase max health and heal the amountIncrease for player
               SetMaxHealth(maxHealth + amountIncrease);
               playerController.OnHeal(amountIncrease);
               break;
            case PlayerStatType.DAMAGE:
               //Update Damage
               SetDamage(damage + amountIncrease);
               playerController.UpdateDamage(damage);
               break;
            case PlayerStatType.SHIELDS:
               // Update new max shield then restore current shield of player
               SetShields(shields + (int)amountIncrease);
               playerController.RestoreShields();
               break;
            case PlayerStatType.CDR:
               // Update cdr
               SetNewCdr(cdr + amountIncrease);
               break;
            default:
               break;
        }
    }

    public void OnInit()
    {
        
    }

    public void OnDeSpawn()
    {
        // Reset all stat to base stat
        damage = 20f;
        maxHealth = 100f;
        shields = 0;
        cdr = 0;
        maxCdr = 0;
        speedMove = 5;
    }

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }




}

public enum PlayerStatType
{
    HEALTH,
    DAMAGE,
    SHIELDS,
    CDR
}
