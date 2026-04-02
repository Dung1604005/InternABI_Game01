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

    public void OnInit()
    {
        
    }

    public void OnDeSpawn()
    {
        damage = 20f;
        maxHealth = 100f;
        shields = 0;
        cdr = 0;
        maxCdr = 0;
    }


}
