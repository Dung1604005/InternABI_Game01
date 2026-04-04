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

    public void SetCoin(int coint)
    {
        coinText.text = coint.ToString();
    }

    public void SetShield(int shield)
    {
        //Update text
        shieldText.text = "x" +  shield.ToString();
    }
}
