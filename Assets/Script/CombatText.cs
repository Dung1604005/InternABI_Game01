using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    public void OnInit(float damage)
    {
        //Init damage text
        damageText.text = damage.ToString();
    }

    public void OnDeSpawn()
    {
        Destroy(gameObject);
    }
}
