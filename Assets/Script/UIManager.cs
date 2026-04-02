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

    public void SetCoin(int coint)
    {
        coinText.text = coint.ToString();
    }
}
