using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;

    [SerializeField] private Vector3 offset;

    private Transform target;

    float hp;
    float maxHp;

    
    public void OnInit(float maxHp, Transform target)
    {
        //Set maxHp and current hp for healthBar
        // Init healthBar with 100% 
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
        // Init transform of character for healthBar
        this.target = target;
    }

    public void SetNewHp(float hp)
    {
        //Update new hp
        this.hp = hp;
    }
    public void SetNewMaxHp(float maxHp)
    {
        this.maxHp = hp;
    }

    void Update()
    {
        // smoothly update health bar fill amount
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp, Time.deltaTime*5f);
        transform.position = target.position +  offset;
    }
    
}
