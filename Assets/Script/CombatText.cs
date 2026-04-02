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

        // Destroy object after 1.5s
        Invoke(nameof(OnDeSpawn), 1.5f);
    }

    public void OnDeSpawn()
    {
        Destroy(gameObject);
    }
}
