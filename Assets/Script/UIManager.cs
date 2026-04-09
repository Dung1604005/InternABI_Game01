using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            // Create only one object with this class
            if(instance == null)
            {
                instance = FindFirstObjectByType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField] private TextMeshProUGUI  coinText;

    [SerializeField] private TextMeshProUGUI shieldText;

    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private TextMeshProUGUI killedEnemyText;

    [SerializeField] private TextMeshProUGUI totalEnemyText;

    private EventBinding<OnLevelChanged> levelChangeBinding;

   

    public void OnEnable()
    {
        EventBus<OnLevelChanged>.Register(levelChangeBinding);
        
    }

    public void OnDisable()
    {
        EventBus<OnLevelChanged>.DeRegister(levelChangeBinding);
        
    }

    public void SetCoin(int coint)
    {
        coinText.text = coint.ToString();
    }

    public void SetShield(int shield)
    {
        //Update text
        shieldText.text = "x" +  shield.ToString();
    }

    public void SetInformationLevel(OnLevelChanged onLevelChanged)
    {
        levelText.text = "Level: "+ onLevelChanged.level.ToString();
        
    }

    
}
